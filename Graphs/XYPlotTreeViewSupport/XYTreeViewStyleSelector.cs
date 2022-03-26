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


namespace Graphs
{
	public class XYTreeViewStyleSelector : StyleSelector
	{
		Style m_TopStyle;
		Style m_XYFunctionStyle;

		public XYTreeViewStyleSelector()
		{
			m_TopStyle = new Style( typeof( TreeViewItem ) );
			m_TopStyle.Setters.Add( new Setter( TreeViewItem.IsExpandedProperty, new Binding( "IsExpanded" ) ) );

			m_XYFunctionStyle = new Style( typeof( TreeViewItem ) );
			m_XYFunctionStyle.Setters.Add( new Setter( TreeViewItem.IsSelectedProperty, new Binding( "IsSelected" ) ) );
		}

		public override Style SelectStyle( object item, DependencyObject container )
		{
			Style s;

			if ( item.GetType() == typeof( XYFunctionViewModel ) )
			{
				s = m_XYFunctionStyle;
			}
			else
			{
				s = m_TopStyle;
			}

			return s;
		}
	}
}
