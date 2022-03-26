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
using System.Linq;

using NetPlus;
using MathUtils;
using MarketData;
using Graphs;

namespace MiAmp
{
	class MiAmp : Window
	{
		protected XYPlotManager m_PlotManager = new XYPlotManager();

		[STAThread]
		public static void Main()
		{
			Application app = new Application();
			app.Run( new MiAmp() );
		}


		public MiAmp()
		{
			Title = "MiAmp";

			// Later get these from a theme.
			Background = Brushes.Black;
			FontFamily = new FontFamily( "Verdana" );

			AllowDrop = true;
			DragDrop.AddDragEnterHandler( this, OnDragEnter );
			DragDrop.AddDragOverHandler( this, OnDragOver );
			DragDrop.AddDragLeaveHandler( this, OnDragLeave );
			DragDrop.AddDropHandler( this, OnDrop );

			Content = m_PlotManager;
		}


		#region Drag drop handing.

		protected bool m_CanDrop = false;

		protected void OnDragEnter( object sender, DragEventArgs e )
		{
			if ( e.Data.GetDataPresent( DataFormats.FileDrop ) )
			{
				string[] filePaths = e.Data.GetData( DataFormats.FileDrop ) as string[];
				m_CanDrop = MDTimeSeriesFactory.hasValidFiles( filePaths );
				if ( m_CanDrop )
				{
					e.Effects = DragDropEffects.Copy;
					e.Handled = m_CanDrop;
				}
			}
		}

		protected void OnDragOver( object sender, DragEventArgs e )
		{
			if ( m_CanDrop )
			{
				e.Effects = DragDropEffects.Copy;
				e.Handled = m_CanDrop;
			}
		}


		protected void OnDragLeave( object sender, DragEventArgs e )
		{
			m_CanDrop = false;
		}


		protected void OnDrop( object sender, DragEventArgs e )
		{
			if ( m_CanDrop )
			{
				PureTree<string, IXYFunctionBase> xyFunctionTree = MDTimeSeriesFactory.createXYFunctionTree( e.Data.GetData( DataFormats.FileDrop ) as string[] );
				m_PlotManager.Add( xyFunctionTree );
			}
		}

		#endregion Drag drop handling.
	}
}