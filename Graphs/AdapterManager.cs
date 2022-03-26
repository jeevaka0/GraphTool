using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NetPlus;
using MathUtils;

namespace Graphs
{
	public class AdapterManager
	{
		public List<IXYAdapter> m_Adapters = new List<IXYAdapter>();
		public IXAxis m_XAxis;								// Could be a list if overlapping multiple plots? Not as important as y axis.
		public List<IYAxis> m_YAxes = new List<IYAxis>();		// Initially we assume that there is only one element in this list.

		public void Add( IXYFunctionBase ixyFunction )		// Precondition: ixyFunction must match current x axis.
		{
			IXYAdapter ixyAdapter = createAdapter( ixyFunction );
			m_Adapters.Add( ixyAdapter );

			Type xType = XYFunction.getXType( ixyFunction );
			if ( m_XAxis == null )
			{
				m_XAxis = createXAxis( xType );
			}
			m_XAxis.Add( ixyAdapter );

			Type yType = XYFunction.getYType( ixyFunction );
			int yIndex = m_YAxes.FindIndex( y => y.getType() == yType );
			IYAxis yAxis;
			if ( -1 == yIndex )
			{
				yAxis = createYAxis( yType );
				m_YAxes.Add( yAxis );
			}
			else
			{
				yAxis = m_YAxes[yIndex];
			}
			yAxis.Add( ixyAdapter );
		}


		public void Remove( IXYFunctionBase ixyFunction )		// Precondition: ixyFunction must match current x axis.
		{
			int index = m_Adapters.FindIndex( x => x.Function == ixyFunction );
			IXYAdapter adapter = m_Adapters[index];

			m_Adapters.RemoveAt( index );
			m_XAxis.Remove( adapter );

			foreach ( IYAxis y in m_YAxes )
			{
				if ( y.Remove( adapter ) && 0 == y.Count )
				{
					m_YAxes.Remove( y );
					break;
				}
			}
		}
	

		IXYAdapter createAdapter( IXYFunctionBase ixyFunction )
		{
			Type objectType = ixyFunction.GetType();
			Type[] xyTypes = objectType.GetInterface( "IXYFunction`2" ).GetGenericArguments();
			Type xyAdapterType = typeof( XYAdapter<,> ).MakeGenericType( xyTypes );
			IXYAdapter ixyAdapter = Activator.CreateInstance( xyAdapterType, ixyFunction ) as IXYAdapter;
			return ixyAdapter;
		}


		IXAxis createXAxis( Type t )
		{
			IXAxis xAxis = null;
			if ( t == typeof( double ) )
			{
				xAxis = new XAxisDouble();
			}
			/*
			else if ( t == typeof(  ) )
			{
			}
			*/
			else
			{
				throw new Exception( "Type " + t.ToString() + " is not a supported x-axis type." );
			}
			return xAxis;
		}


		IYAxis createYAxis( Type t )
		{
			IYAxis yAxis = null;
			if ( t == typeof( double ) )
			{
				yAxis = new YAxisDouble();
			}
			/*
			else if ( t == typeof(  ) )
			{
			}
			*/
			else
			{
				throw new Exception( "Type " + t.ToString() + " is not a supported y-axis type." );
			}
			return yAxis;
		}
	}
}
