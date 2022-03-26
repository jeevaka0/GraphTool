using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NetPlus;
using MathUtils;

// Groups a set of x-series and do the conversion between real-X and screen-X coordinates.

namespace Graphs
{
	// This implemnts most methods in IAxis, but we leave X and Y Axis to derive from this and implemnt IXAxis and IYAxis which inherit from IAxis.

	public abstract class Axis<T, Adapter> where Adapter : class
	{
		protected List<Adapter> m_XAdapters = new List<Adapter>();
		internal List<T> m_TickValues = new List<T>();

		public double m_ScreenMin;
		public double m_ScreenMax;

		internal T m_CurrentMin;
		internal T m_CurrentMax;


		public double ScreenMin
		{
			set { m_ScreenMin = value; }
		}

		public double ScreenMax
		{
			set { m_ScreenMax = value; }
		}

		public Type getType()
		{
			return typeof( T );
		}

		public virtual void Add( IXYAdapter xyAdapter )
		{
			m_XAdapters.Add( xyAdapter as Adapter );
		}

		public virtual bool Remove( IXYAdapter xyAdapter )
		{
			return m_XAdapters.Remove( xyAdapter as Adapter );
		}

		public int Count
		{
			get { return m_XAdapters.Count; }
		}


		// We dont use type specific derived classes to avoid having to repeat code.
		virtual public List<string> getLabels()
		{
			throw new Exception( typeof( T ).ToString() + " type is not a supported axis type." );
		}

	}
}
