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
	// XYAdapter wraps XYFunction objects. I.e. emperical functions. If we need to display other functions, then create a matching wrapper type.
	// Also they may not have min / max values to initialize the x axis. So we will need a way to fix initial x range.
	// Moreover, they will not be able to plot the points they way emperical functions do. (I.e. the number of points is infinite. Select a suitable x increment.)

	// Later this will have a tree view containing all the graphs added and along with tick boxes and contect menu to manipulate.

	public class XYPlotManager : DockPanel
	{
		public TreeView m_PlotSelector = new TreeView();
		XYPlotTreeViewViewModel m_TreeViewModel = new XYPlotTreeViewViewModel();
		XYTreeViewTemplateSelector m_TemplateSelector = new XYTreeViewTemplateSelector();
		XYTreeViewStyleSelector m_StyleSelector = new XYTreeViewStyleSelector();

		public XYCanvas m_Canvas = new XYCanvas();							

		public XYPlotManager()
		{
			SetupTreeView();

			m_PlotSelector.Padding = new Thickness( 0, 10, 10, 10 );
			Children.Add( m_PlotSelector );
			DockPanel.SetDock( m_PlotSelector, Dock.Left );

			Children.Add( m_Canvas );
		}


		protected void SetupTreeView()
		{
			// Tree view is two layered for now.
			// Top layer contains XYFunction collection names. Next layer contains XYFunction names.

			m_PlotSelector.ItemsSource = m_TreeViewModel;

			m_PlotSelector.ItemTemplateSelector = m_TemplateSelector;
			m_PlotSelector.ItemContainerStyleSelector = m_StyleSelector;

			// We could setup a secondary template for the second level?
		}


		// For the first cut we add selected time series from the first dictionary.
		public void Add( PureTree<string, IXYFunctionBase> xyFunctionTree )
		{
			AddHelper( xyFunctionTree, m_TreeViewModel );
			
			//m_Canvas.InvalidateVisual();
		}


		public void Select( IXYFunctionBase ixyFunction )
		{
			m_Canvas.Add( ixyFunction );
		}


		public void Unselect( IXYFunctionBase ixyFunction )
		{
			m_Canvas.Remove( ixyFunction );
		}

		protected void AddHelper( PureTree<string, IXYFunctionBase> xyFunctionTree, ObservableCollection<Object> xyFunctionTreeViewModel )
		{
			foreach ( KeyValuePair<string, Object> kv in xyFunctionTree )
			{
				if ( kv.Value.GetType().GetInterfaces().Contains( typeof( IXYFunctionBase ) ) )
				{
					xyFunctionTreeViewModel.Add( new XYFunctionViewModel( kv.Key, kv.Value as IXYFunctionBase, this ) );
				}
				else	// For now the only other type is another tree.
				{
					PureTree<string, IXYFunctionBase> childTree = kv.Value as PureTree<string, IXYFunctionBase>;
					XYFunctionTreeViewModel childTreeViewModel = new XYFunctionTreeViewModel( kv.Key, childTree );
					childTreeViewModel.IsExpanded = xyFunctionTree.First().Key == kv.Key;								// Expand the first one at each level.
					xyFunctionTreeViewModel.Add( childTreeViewModel );

					AddHelper( childTree, childTreeViewModel );
				}
			}
		}
	}
}
