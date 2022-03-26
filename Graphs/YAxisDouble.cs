using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Graphs
{
	public class YAxisDouble : YAxis<double>
	{
		public override List<string> getLabels()
		{
			return DoubleTickCalculator.getTicks( m_CurrentMin, m_CurrentMax, m_TickValues );
		}
	}
}
