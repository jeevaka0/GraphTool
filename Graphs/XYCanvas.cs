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

namespace Graphs
{
	public class XYCanvas : Canvas
	{
		// These could be moved to axis level later.
		public int m_LargeTick = 12;
		public int m_SmallTick;
		public int m_MidTick;

		public Typeface m_Typeface;
		public double m_FontSize;

		protected AdapterManager m_AdapterManager = new AdapterManager();

		public XYCanvas()
		{
			m_SmallTick = m_LargeTick / 2;
			m_MidTick = ( m_LargeTick * 3 ) / 2;

			m_Typeface = new Typeface( "Verdana" );
			m_FontSize = 48;

		}

		protected IXAxis X
		{
			get { return m_AdapterManager.m_XAxis; }
		}


		// For now we are dealing only with the primary Y axis.
		protected IYAxis Y
		{
			get { return m_AdapterManager.m_YAxes[0]; }
		}


		protected List<IXYAdapter> Adapters
		{
			get { return m_AdapterManager.m_Adapters; }
		}


		public void Add( IXYFunctionBase ixyFunction )
		{
			m_AdapterManager.Add( ixyFunction );
			InvalidateVisual();
		}


		public void Remove( IXYFunctionBase ixyFunction )
		{
			m_AdapterManager.Remove( ixyFunction );
			InvalidateVisual();
		}


		public void SetPlots( IAxis xAxis, List<IAxis> yAxes, List<IXYAdapter> xyAdapters )
		{
			// Get Label sizes and decide the xy area for the plot.


			// Set x range on x axis and y range of y axes.


			// Get plots from XYAdapters.
		}

		protected override void OnRender( DrawingContext dc )
		{
			base.OnRender( dc );

			if ( 0 != Adapters.Count )
			{
				List<string> xLabels = X.getLabels();

				FormattedText xLabel = new FormattedText( xLabels[0], CultureInfo.CurrentCulture, FlowDirection.LeftToRight, m_Typeface, m_FontSize, Brushes.White );
				dc.DrawText( xLabel, new Point( 10, 10 ) );
			}
		}
	}
}
