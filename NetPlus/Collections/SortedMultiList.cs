using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetPlus
{
	public class SortedMultiList<Key, Value>
	{
		protected List<Key> m_Keys = new List<Key>();
		protected List<Value> m_Values = new List<Value>();

		public int Count
		{
			get { return m_Keys.Count; }
		}

		public IList<Key> Keys
		{
			get { return m_Keys; }
		}


		public IList<Value> Values
		{
			get { return m_Values; }
		}

		

		// Precondition: k must be larger that or equal to the last key in the list.
		public virtual void Append( Key k, Value v )
		{
			m_Keys.Add( k );
			m_Values.Add( v );
		}


	}
}
