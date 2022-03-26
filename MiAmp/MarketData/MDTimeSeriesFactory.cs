using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;

using NetPlus;
using MathUtils;
using SystemUtils;

namespace MarketData
{
	public enum MDSeriesType { None, BBO, RawVolume, LevelThinningSafeMins };


	public class MDTimeSeriesFactory
	{
		public static bool hasValidFiles( string[] filePaths )
		{
			bool yes = false;

			foreach ( string s in filePaths )
			{
				FileInfo fileInfo = new FileInfo( s );

				yes = yes || fileMDSeriesType( fileInfo ) != MDSeriesType.None;
			}

			return yes;
		}


		// At least one file must be a valid file.
		public static PureTree<string, IXYFunctionBase> createXYFunctionTree( string[] filePaths )
		{
			PureTree<string, IXYFunctionBase> xyFunctionTree = new PureTree<string, IXYFunctionBase>();

			foreach ( string s in filePaths )
			{
				FileInfo fileInfo = new FileInfo( s );
				MDSeriesType seriesType = fileMDSeriesType( fileInfo );
				string name = FileUtils.getFileName( fileInfo );
				switch ( seriesType )
				{
					case MDSeriesType.BBO:
						xyFunctionTree[name] = createBBODictionary( fileInfo );
						break;
					case MDSeriesType.LevelThinningSafeMins:
						xyFunctionTree[name] = createLevelThinningSafeMinTree( fileInfo );
						break;
					default:
						break;
				}
			}

			return xyFunctionTree;
		}


		protected static PureTree<string, IXYFunctionBase> createBBODictionary( FileInfo fileInfo )
		{
			TimeSeries<DateTime> bidLocalTimes = new TimeSeries<DateTime>();
			TimeSeries<DateTime> offerLocalTimes = new TimeSeries<DateTime>();
			TimeSeries<decimal> bidPrices = new TimeSeries<decimal>();
			TimeSeries<ulong> bidSizes = new TimeSeries<ulong>();
			TimeSeries<decimal> offerPrices = new TimeSeries<decimal>();
			TimeSeries<ulong> offerSizes = new TimeSeries<ulong>();

			PureTree<string, IXYFunctionBase> bboTree = new PureTree<string, IXYFunctionBase>();
			bboTree.Add( "BidLocalTime", bidLocalTimes );
			bboTree.Add( "OfferLocalTime", offerLocalTimes );
			bboTree.Add( "BidPrices", bidPrices );
			bboTree.Add( "BidSizes", bidSizes );
			bboTree.Add( "OfferPrices", offerPrices );
			bboTree.Add( "OfferSizes", offerSizes );


			StreamReader textReader = new StreamReader( fileInfo.FullName, Encoding.ASCII );
			textReader.ReadLine();																	// Discard header.

			while ( !textReader.EndOfStream )
			{
				string line = textReader.ReadLine();
				string[] tokens = line.Split( new Char[]{'\t'}, StringSplitOptions.RemoveEmptyEntries );

				DateTime localTime = DateTime.Parse( tokens[0] );
				DateTime bidTime = DateTime.Parse( tokens[1] );
				ulong bidSize = UInt64.Parse( tokens[2] );
				decimal bidPrice = Decimal.Parse( tokens[3] );
				decimal offerPrice = Decimal.Parse( tokens[4] );
				ulong offerSize = UInt64.Parse( tokens[5] );
				DateTime offerTime = DateTime.Parse( tokens[6] );

				//bool sameBidTimestamp = bidSizes.Keys[bidSizes.Count - 1].m_DateTime == bidTime;
				
				// If there are sizes for two prices at the same timestamp, we loose that information when we split into separate series.

				bool bidChange = bidPrices.Append( bidTime, bidPrice ) || bidSizes.Append( bidTime, bidSize );
				bool offerChange = offerPrices.Append( offerTime, offerPrice ) || offerSizes.Append( offerTime, offerSize );

				if ( bidChange )
				{
					bidLocalTimes.Append( bidTime, localTime );
				}
				if ( offerChange )
				{
					offerLocalTimes.Append( offerTime, localTime );
				}
			}

			return bboTree;
		}


		// Create the cumulative probability distributions.
		protected static PureTree<string, IXYFunctionBase> createLevelThinningSafeMinTree( FileInfo fileInfo )
		{
			PureTree<string, IXYFunctionBase> distributionTree = new PureTree<string, IXYFunctionBase>();

			// First we need to read the columns into Univariate data sets.
			List<IParseAndPass> parsers = new List<IParseAndPass>();
			PureTree<string, IUnivariateBase> univariateTree = new PureTree<string, IUnivariateBase>();

			parsers.Add( new NullParseAndPass() );				// Skip time
			parsers.Add( new NullParseAndPass() );				// Skip side for now.

			univariateTree.Add( "Thinning Side", createLevelThinningSafeMinsSideSubTree( parsers ) );
			univariateTree.Add( "Opposite Side", createLevelThinningSafeMinsSideSubTree( parsers ) );

			TextTableParser tableParser = new TextTableParser( fileInfo.FullName );
			tableParser.Parse( parsers, true );

			univariateTree.invokeOnLeaves( x => x.Sort() );

			// Then create distributions (xy-functions) out of them.

			distributionTree.Add( "CDF", univariateTree.createTree<IXYFunctionBase>( x => x.getCDF() ) );
			distributionTree.Add( "PDF", univariateTree.createTree<IXYFunctionBase>( x => x.getPDF() ) );

			return distributionTree;
		}


		protected static PureTree<string, IUnivariateBase> createLevelThinningSafeMinsSideSubTree( List<IParseAndPass> parsers )
		{
			PureTree<string, IUnivariateBase> side = new PureTree<string, IUnivariateBase>();
			side.Add( "Thinning Rates", createLevelThinningSafeMinsSubTree( parsers ) );
			side.Add( "Trades WEMA", createLevelThinningSafeMinsSubTree( parsers ) );
			side.Add( "Ratio", createOneUnivariate( parsers ) );
			return side;
		}


		protected static PureTree<string, IUnivariateBase> createLevelThinningSafeMinsSubTree( List<IParseAndPass> parsers )
		{
			string[] names = { "200ms", "500ms", "1s", "2s", "5s", "10s", "30s", "1min" };

			PureTree<string, IUnivariateBase> subTree = new PureTree<string, IUnivariateBase>();
			foreach ( string s in names )
			{
				Univariate<double> univariate = createOneUnivariate( parsers );
				subTree.Add( s, univariate );
			}

			return subTree;
		}


		protected static Univariate<double> createOneUnivariate( List<IParseAndPass> parsers )
		{
			Univariate<double> univariate = new Univariate<double>();
			parsers.Add( new DoubleParseAndPass( new Pass<double>( univariate.Add ) ) );
			return univariate;
		}



		protected static MDSeriesType fileMDSeriesType( FileInfo fileInfo )
		{
			string fileName = FileUtils.getFileName( fileInfo );
			string[] mdSeriesTypeNames = Enum.GetNames( typeof( MDSeriesType ) );
			string name = Array.Find( mdSeriesTypeNames, e => fileName.EndsWith( e ) );
			return (MDSeriesType) Enum.Parse( typeof( MDSeriesType ), name );
		}


		protected static string fileSeriesName( FileInfo fileInfo, MDSeriesType seriesType )
		{
			string fileName = FileUtils.getFileName( fileInfo );
			int typeIndex = fileName.LastIndexOf( seriesType.ToString() );
			return fileName.Substring( 0, typeIndex );
		}
	}
}
