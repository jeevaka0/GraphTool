using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MathUtils
{
	public class TimeSeries<V> : XYFunction<TimeKey, V>
	{
		//public SortedList<TimeKey, V> m_Points = new ();

		// Appends the given <k, v> pair if it is not a duplicated.
		public virtual bool Append( DateTime k, V v )
		{
			bool added = true;
			if ( 0 == Count )
			{
				Append( new TimeKey( k, 0 ), v );
			}
			else
			{
				TimeKey lastKey = Keys[Count - 1];
				if ( lastKey.m_DateTime == k )				// If the timestamp is the same
				{
					if ( EqualityComparer<V>.Default.Equals( Values[Count - 1], v ) )			// but the value is different ...
					{
						Append( new TimeKey( k, lastKey.m_Ordinal + 1 ), v );
					}
					else
					{
						added = false;
					}
				}
				else
				{
					Append( new TimeKey( k, 0 ), v );
				}
			}
			return added;
		}

	}
}
