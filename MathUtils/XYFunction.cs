using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NetPlus;

namespace MathUtils
{
	// This is the raw format containing explicit values.
	public class XYFunction<XType, YType> : SortedMultiList<XType, YType>, IXYFunction<XType, YType>
	{
		public XType MinX
		{
			get { return Keys[0]; }
		}

		public XType MaxX
		{
			get { return Keys[Count-1]; }
		}
	}


	// Static helpers.
	public class XYFunction
	{
		// These would be much simpler if we could impmenent them at 'IXYFunction<XType, YType>' level.
		public static Type getXType( IXYFunctionBase ixy )
		{
			return getTypeN( ixy, 0 );
		}

		public static Type getYType( IXYFunctionBase ixy )
		{
			return getTypeN( ixy, 1 );
		}

		protected static Type getTypeN( IXYFunctionBase ixy, int n )
		{
			return ixy.GetType().GetInterface( "IXYFunction`2" ).GetGenericArguments()[n];
		}
	}
}
