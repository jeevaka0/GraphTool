using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Graphs
{
	public interface IAxis
	{
		double ScreenMin
		{
			set;
		}

		double ScreenMax
		{
			set;
		}

		Type getType();
		void Add( IXYAdapter xyAdapter );
		bool Remove( IXYAdapter xyAdapter );
		int Count { get; }

		List<string> getLabels();
	}
}
