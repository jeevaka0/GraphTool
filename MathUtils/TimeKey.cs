using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MathUtils
{
	public class TimeKey : IComparable, IComparable<TimeKey>
	{
		public DateTime m_DateTime;
		public ulong m_Ordinal;


		public TimeKey( DateTime dateTime, ulong ordinal )
		{
			m_DateTime = dateTime;
			m_Ordinal = ordinal;
		}


		public int CompareTo( object o )
		{
			return CompareTo( o as TimeKey );
		}

		public int CompareTo( TimeKey other )
		{
			int result = m_DateTime.CompareTo( other.m_DateTime );

			if ( 0 == result )
			{
				result = m_Ordinal.CompareTo( other.m_Ordinal );
			}

			return result;
		}
	}
}
