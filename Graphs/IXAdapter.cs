using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Graphs
{
	public interface IXAdapter<T>
	{
		T AbsoluteMinX
		{
			get;
		}

		T AbsoluteMaxX
		{
			get;
		}
	}
}
