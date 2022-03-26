using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace SystemUtils
{
	public class FileUtils
	{
		public static string getFileName( FileInfo fileInfo )
		{
			return fileInfo.Name.Remove( fileInfo.Name.LastIndexOf( fileInfo.Extension ) );
		}

	}
}
