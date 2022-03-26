using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NetPlus;

namespace MathUtils
{
	public interface IUnivariateBase
	{
		int Count
		{
			get;
		}

		IXYFunctionBase getCDF();
		IXYFunctionBase getPDF();

		void Sort();
	}
}
