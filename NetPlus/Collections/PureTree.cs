using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetPlus
{
	public class PureTree<Key, T> : ObjectTree<Key> where T : class
	{
		public PureTree<Key, Target> createTree<Target>( DelegateTypes.Convert<T, Target> converter ) where Target : class
		{
			PureTree<Key, Target> tree = new PureTree<Key, Target>();

			foreach ( KeyValuePair<Key, Object> kv in this )
			{
				Object branch;
				if ( kv.Value.GetType().GetInterfaces().Contains( typeof( T ) ) )
				{
					branch = converter( ( kv.Value as T ) );
				}
				else
				{
					branch = ( kv.Value as PureTree<Key, T> ).createTree<Target>( converter );
				}
				tree.Add( kv.Key, branch );
			}

			return tree;
		}


		public void invokeOnLeaves( DelegateTypes.Invoke<T> invoker )
		{
			foreach ( KeyValuePair<Key, Object> kv in this )
			{
				if ( kv.Value.GetType().GetInterfaces().Contains( typeof( T ) ) )
				{
					invoker( kv.Value as T );
				}
				else
				{
					( kv.Value as PureTree<Key, T> ).invokeOnLeaves( invoker );
				}
			}
		}
	}
}
