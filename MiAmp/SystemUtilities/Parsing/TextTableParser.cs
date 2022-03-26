using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;


namespace SystemUtils
{
	public class TextTableParser
	{
		StreamReader m_TextReader;

		public TextTableParser( string fullFileName )
		{
			m_TextReader = new StreamReader( fullFileName, Encoding.ASCII );
		}

		public string[] getHeaders()
		{
			string line = m_TextReader.ReadLine();
			string[] tokens = line.Split( new Char[] { '\t' }, StringSplitOptions.RemoveEmptyEntries );
			return tokens;
		}


		public void Parse( List<IParseAndPass> parsers, bool skipFirstLine )
		{
			if ( skipFirstLine )
			{
				m_TextReader.ReadLine();																	// Discard header.
			}

			while ( !m_TextReader.EndOfStream )
			{
				string line = m_TextReader.ReadLine();
				string[] tokens = line.Split( new Char[] { '\t' }, StringSplitOptions.RemoveEmptyEntries );
				if ( tokens.Length == parsers.Count )
				{
					for ( int i = 0; i < tokens.Length; i++ )
					{
						parsers[i].Receive( tokens[i] );
					}
				}
				else
				{
					throw new Exception( "Number of tokens does not match the number or parsers. " + parsers.Count + " parsers for line: " + line );
				}
			}
		}
	}
}
