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

using MathUtils;

namespace Graphs
{
	public class XYFunctionViewModel
	{
		protected bool m_Selected;
		protected bool m_Checked;
		protected string m_Name;
		protected IXYFunctionBase m_IXYFunctionBase;
		protected XYPlotManager m_PlotManager;

		public string Name
		{
			get { return m_Name; }
		}

		public bool IsSelected
		{
			get { return m_Selected; }
			set { m_Selected = value; }
		}


		public bool IsChecked
		{
			get { return m_Checked; }
			set 
			{
				m_Checked = value;
				if ( m_Checked )
				{
					m_PlotManager.Select( m_IXYFunctionBase );
				}
				else
				{
					m_PlotManager.Unselect( m_IXYFunctionBase );
				}
			}
		}


		public XYFunctionViewModel( string name, IXYFunctionBase ixyFunctionBase, XYPlotManager plotManager )
		{
			m_Name = name;
			m_IXYFunctionBase = ixyFunctionBase;
			m_PlotManager = plotManager;
		}
	}
}
