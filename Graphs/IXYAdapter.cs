using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NetPlus;
using MathUtils;

namespace Graphs
{
	public interface IXYAdapter
	{
		IXYFunctionBase Function
		{
			get;
		}
	}
}
