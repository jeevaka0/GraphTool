using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using NetPlus;
using MathUtils;

namespace Graphs
{
	public class XAxis<T> : Axis<T, IXAdapter<T>>, IXAxis
	{
		// We could cache these if it take too much time.
		protected T AbsoluteMin
		{
		    get
		    {
				return m_XAdapters.Aggregate( m_XAdapters[0].AbsoluteMinX, ( result, adapter ) => Operators<T>.Min( result, adapter.AbsoluteMinX ) );
		    }
		}


		protected T AbsoluteMax
		{
			get
			{
				return m_XAdapters.Aggregate( m_XAdapters[0].AbsoluteMaxX, ( result, adapter ) => Operators<T>.Max( result, adapter.AbsoluteMaxX ) );
			}
		}


		// If this is the first adapter or if the current Min and Max are the same as absoulute values (i.e. user haven't zoomed) then set the current Min and Max to be 
		// absolute ones.
		public override void Add( IXYAdapter xyAdapter )
		{
			bool updateCurrentValues = 0 == m_XAdapters.Count || ( Operators<T>.Equal( m_CurrentMin, AbsoluteMin ) && Operators<T>.Equal( m_CurrentMax, AbsoluteMax ) );
			base.Add( xyAdapter );
			if ( updateCurrentValues )
			{
				m_CurrentMin = AbsoluteMin;
				m_CurrentMax = AbsoluteMax;
			}
		}
	}
}
