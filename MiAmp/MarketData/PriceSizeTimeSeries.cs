using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NetPlus;
using MathUtils;

namespace MarketData
{
	public class PriceSizeTimeSeries : PureTree<string, IXYFunctionBase>
	{
		public const string Size = "Size";
		public const string Price = "Price";
		public const string LocalTime = "LocalTime";

		public string m_Name;

		protected TimeSeries<decimal> m_Prices = new TimeSeries<decimal>();
		protected TimeSeries<ulong> m_Sizes = new TimeSeries<ulong>();
		protected TimeSeries<DateTime> m_LocalTimes = new TimeSeries<DateTime>();

		public PriceSizeTimeSeries( string name )
		{
			m_Name = name;
			
			Add( Size, m_Sizes );
			Add( Price, m_Prices );
			Add( LocalTime, m_LocalTimes );
		}


		// This is part of the initialization process. This is called repeatedly with the 4 tuples with time monotonically increasing.
		public void add( DateTime time, decimal price, ulong size, DateTime localTime )
		{

		}
	}
}
