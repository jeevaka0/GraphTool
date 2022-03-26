using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Graphs
{
	public class XAxisDateTime : XAxis<DateTime>
	{
		// Later we can use the X range to decide the exact format. For now we are going to assume intraday data.
		public override string getLabelFormat()
		{
			return "HH:MM:SS.000";
		}

	}
}
