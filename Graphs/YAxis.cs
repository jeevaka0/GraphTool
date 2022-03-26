using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Graphs
{
	public abstract class YAxis<T> : Axis<T, IYAdapter<T>>, IYAxis
	{
	}
}
