using System;
using System.IO;
using System.ComponentModel;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using System.Windows.Shapes;
using System.Windows.Documents;
using System.Windows.Data;
using System.Text;
using System.Diagnostics;
using System.Globalization;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;


namespace NetPlus
{
	public class TypeFilters
	{
		protected static TypeFilter m_FilterOneType = new TypeFilter( FilterOneType );

		public static TypeFilter FilterOne
		{
			get { return m_FilterOneType; }
		}

		public static bool FilterOneType( Type typeObj, Object criteriaObj )
		{
			return typeObj == criteriaObj as Type;
		}

	}
}
