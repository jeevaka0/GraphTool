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

using NetPlus;
using MathUtils;


namespace Graphs
{
	public class XYFunctionTreeViewModel : ObservableCollection<Object>
	{
		protected string m_Name;
		protected bool m_Expanded;
		protected PureTree<string, IXYFunctionBase> m_Tree; 

		public XYFunctionTreeViewModel( string name, PureTree<string, IXYFunctionBase> tree )
		{
			m_Name = name;
			m_Expanded = true;
			m_Tree = tree;
		}

		public string Name
		{
			get { return m_Name; }
		}

		public bool IsExpanded
		{
			get { return m_Expanded; }
			set { m_Expanded = value; }
		}
	}
}
