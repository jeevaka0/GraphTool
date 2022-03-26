using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SystemUtilities
{
	public class Int32ParseAndPass : ParseAndPass<Int32>
	{
		public Int32ParseAndPass( Pass<Int32> passer )
			: base( passer, new ParserType<Int32>( Int32.Parse ) )
		{
		}
	}
}
