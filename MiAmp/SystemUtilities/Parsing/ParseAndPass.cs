using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SystemUtils
{
	public delegate void Pass<T>( T value );
	public delegate T ParserType<T>( string s );

	public class ParseAndPass<T> : IParseAndPass
	{
		protected Pass<T> m_Passer;
		protected ParserType<T> m_Parser;

		public ParseAndPass( Pass<T> passer, ParserType<T> parser )
		{
			m_Passer = passer;
			m_Parser = parser;
		}

		public void Receive( string s )
		{
			T value = m_Parser( s );
			m_Passer( value );
		}
	}
}
