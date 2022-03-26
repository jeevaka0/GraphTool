using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NetPlus;

namespace MathUtils
{
	// For now assume that we add all the values first and then call initialize where we do the sort / calculations. 
	// Later we might want to keep 'dirty' flags and do the calculations / sorting lazily.
	public class Univariate<T> : List<T>, IUnivariate<T>
	{

		public IXYFunctionBase getCDF()
		{
			return Univariate<T>.getCDF( this );
		}


		public IXYFunctionBase getPDF()
		{
			return Univariate<T>.getPDF( this );
		}

		// Here the number of points in the cdf is the same as the number of values in the univariate.
		// Precondition: univariate must have at least two elements.
		static protected IXYFunction<T, double> getCDF( IUnivariate<T> univariate )
		{
			// At least for now, we create a XYFunction with full data.
			XYFunction<T, double> cdf = new XYFunction<T, double>();

			double delta = (double)1 / ( univariate.Count - 1 );
			uint count = 0;

			foreach ( T t in univariate )
			{
				cdf.Append( t, delta * count++ );				// This way the 'cd' will be almost 1 when 'count' is 'univariate.Count - 1'. i.e. no drift.
			}
			return cdf;
		}

		// Here the number of points in the cdf is the same as the number of values in the univariate.
		// Precondition: univariate must have at least two elements.
		// We run the risk of 'infinite density' at some points. Ideally we need to avoid those points and also use some kind of smoothing.
		static protected IXYFunction<T, double> getPDF( IUnivariate<T> univariate )
		{
			// At least for now, we create a XYFunction with full data and not a wrapper that calculate values when they are requested.
			XYFunction<T, double> pdf = new XYFunction<T, double>();

			double density = 0;
			double twoDelta = (double)2 / ( univariate.Count - 1 );

			IEnumerator<T> enumerator = univariate.GetEnumerator();
			DelegateTypes.Get<double> getValue = () => Operators<T, double>.Cast( enumerator.Current );

			enumerator.MoveNext();
			T lastT = enumerator.Current;

			pdf.Append( lastT, density );				// First point is the first value in the series with density 0.

			while ( enumerator.MoveNext() )
			{
				T difference = Operators<T>.Subtract( enumerator.Current, lastT );

				density = twoDelta / ( Operators<T, double>.Cast( difference ) ) - density;
				lastT = enumerator.Current;
				pdf.Append( enumerator.Current, density );
			}
			return pdf;
		}
	}
}
