using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NetPlus;

namespace MathUtils
{
	/*
	public class PureTree<string, IUnivariateBase> : ObjectTree<string>
	{
		public PureTree<string, IXYFunctionBase> createCDFTree()
		{
			PureTree<string, IXYFunctionBase> tree = new PureTree<string, IXYFunctionBase>();

			foreach ( KeyValuePair<string, Object> kv in this )
			{
				Object branch;
				if ( kv.Value.GetType() == typeof( PureTree<string, IUnivariateBase> ) )
				{
					branch = ( kv.Value as PureTree<string, IUnivariateBase> ).createCDFTree();
				}
				else
				{
					branch = ( kv.Value as IUnivariateBase ).getCDF();
				}
				tree.Add( kv.Key, branch );
			}

			return tree;
		}

		public PureTree<string, IXYFunctionBase> createPDFTree()
		{
			PureTree<string, IXYFunctionBase> tree = new PureTree<string, IXYFunctionBase>();

			foreach ( KeyValuePair<string, Object> kv in this )
			{
				Object branch;
				if ( kv.Value.GetType() == typeof( PureTree<string, IUnivariateBase> ) )
				{
					branch = ( kv.Value as PureTree<string, IUnivariateBase> ).createPDFTree();
				}
				else
				{
					branch = ( kv.Value as IUnivariateBase ).getCDF();
				}
				tree.Add( kv.Key, branch );
			}

			return tree;
		}
	}
	 */
}
