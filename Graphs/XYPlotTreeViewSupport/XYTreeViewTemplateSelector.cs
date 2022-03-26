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

using UIUtils;

namespace Graphs
{
	public class XYTreeViewTemplateSelector : DataTemplateSelector
	{
		protected HierarchicalDataTemplate m_TopTemplate;
		protected HierarchicalDataTemplate m_XYFunctionTemplate;

		public XYTreeViewTemplateSelector()
		{
			m_TopTemplate = new HierarchicalDataTemplate();
			m_TopTemplate.ItemsSource = new Binding( "." );
			FrameworkElementFactory topFactory = new FrameworkElementFactory( typeof( TextBlock ) );
			topFactory.SetBinding( TextBlock.TextProperty, new Binding( "Name" ) );
			m_TopTemplate.VisualTree = topFactory;

			m_XYFunctionTemplate = new HierarchicalDataTemplate();
			m_XYFunctionTemplate.ItemsSource = new Binding( "." );
			FrameworkElementFactory xyFunctionFactory = new FrameworkElementFactory( typeof( TickText ) );
			Binding bindingName = new Binding( "Name" );
			bindingName.Mode = BindingMode.OneWay;
			xyFunctionFactory.SetBinding( TickText.TextProperty, bindingName );
			Binding bindingTicked = new Binding( "IsChecked" );
			bindingTicked.Mode = BindingMode.TwoWay;
			xyFunctionFactory.SetBinding( TickText.IsCheckedProperty, bindingTicked );

			m_XYFunctionTemplate.VisualTree = xyFunctionFactory;
		}


        public override DataTemplate SelectTemplate( object item, DependencyObject container )
		{
			DataTemplate t;

			if ( item.GetType() == typeof( XYFunctionViewModel ) )
			{
				t = m_XYFunctionTemplate;
			}
			else
			{
				t = m_TopTemplate;
			}

			return t;
		}
  
	}
}
