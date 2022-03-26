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


namespace UIUtils
{
	public class TickText : StackPanel
	{
		public static DependencyProperty TextProperty 
		    = DependencyProperty.RegisterAttached( "Text", typeof( string ), typeof( TickText ), new FrameworkPropertyMetadata() );

		public static DependencyProperty IsCheckedProperty
			= DependencyProperty.RegisterAttached( "IsChecked", typeof( bool ), typeof( TickText ), new FrameworkPropertyMetadata() );


		protected CheckBox m_CheckBox;
		protected TextBlock m_TextBlock;

		public bool? Check
		{
			get { return m_CheckBox.IsChecked; }
			set { m_CheckBox.IsChecked = value; }
		}

		public TickText()
		{
			Orientation = Orientation.Horizontal;
			Thickness t = new Thickness( 3 );

			m_CheckBox = new CheckBox();
			m_CheckBox.VerticalAlignment = VerticalAlignment.Center;
			m_CheckBox.Margin = t;
			m_TextBlock = new TextBlock();
			m_TextBlock.Padding = t;

			//m_TextBlock.Margin = m_Margin;
			Children.Add( m_CheckBox );
			Children.Add( m_TextBlock );

			// Binds the 'Text' property of this class to the Text property of TextBlock.
			Binding bindingText = new Binding( "Text" );
			bindingText.Source = this;
			bindingText.Mode = BindingMode.OneWay;
			m_TextBlock.SetBinding( TextBlock.TextProperty, bindingText );

			Binding bindingCheck = new Binding( "IsChecked" );
			bindingCheck.Source = this;
			bindingCheck.Mode = BindingMode.TwoWay;
			m_CheckBox.SetBinding( CheckBox.IsCheckedProperty, bindingCheck );
		}
	}
}
