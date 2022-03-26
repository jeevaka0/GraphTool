using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NetPlus;
using MathUtils;

namespace Graphs
{
	// Introduce intermediate classes if necessary.
	public class XYAdapter<XType, YType> : IXYAdapter, IXAdapter<XType>, IYAdapter<YType>
	{
		public XYFunction<XType, YType> m_XYFunction;

		public IXYFunctionBase Function
		{
			get { return m_XYFunction; }
		}

		public XYAdapter( IXYFunctionBase ixyFunction )
		{
			m_XYFunction = ixyFunction as XYFunction<XType, YType>;	
		}

		public XType AbsoluteMinX
		{
			get
			{
				return m_XYFunction.MinX;
			}
		}

		public XType AbsoluteMaxX
		{
			get
			{
				return m_XYFunction.MaxX;
			}
		}
	}
}
