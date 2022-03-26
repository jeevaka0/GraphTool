using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SystemUtils
{
	public class DoubleParseAndPass : ParseAndPass<Double>
	{
		public DoubleParseAndPass( Pass<Double> passer )
			: base( passer, new ParserType<Double>( Double.Parse ) )
		{
		}
	}
}
