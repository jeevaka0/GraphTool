using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NetPlus;

namespace MathUtils
{
	// An object other than Univariate can appear to be a Univariate by implementing the interface. E.g. Values in a time series.
	public interface IUnivariate<T> : IUnivariateBase, IEnumerable<T>
	{
	}
}
