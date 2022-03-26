using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetPlus
{
	public class DelegateTypes
	{
		public delegate T Get<T>();
		public delegate void Invoke<T>( T t );
		public delegate Target Convert<Source, Target>( Source s );// where Source : class where Target : class;
		public delegate T BinaryFunction<T>( T a, T b );
		public delegate bool Comparison<T>( T a, T b );
	}
}
