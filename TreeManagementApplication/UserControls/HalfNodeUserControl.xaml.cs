using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TreeManagementApplication.UserControls
{
	public enum HalfNodeType { Left, Right }

	public partial class HalfNodeUserControl : UserControl
	{
		bool isActive = true;
		public static readonly DependencyProperty NodeTypeProperty = DependencyProperty.Register("HalfNodeType", typeof(HalfNodeType), typeof(HalfNodeUserControl), new PropertyMetadata(HalfNodeType.Left, NodeTypePropertyChanged));

		public HalfNodeType NodeType
		{
			get { return (HalfNodeType)GetValue(NodeTypeProperty); }
			set { SetValue(NodeTypeProperty, value); }
		}

		public static void NodeTypePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			var control = (HalfNodeUserControl)d;
			control.UpdateCornerRadius();
		}

		public void UpdateCornerRadius()
		{
			if (NodeType == HalfNodeType.Left)
			{
				NodeBorder.CornerRadius = new CornerRadius(37.5, 0, 0, 37.5);
			}
			else if (NodeType == HalfNodeType.Right)
			{
				NodeBorder.CornerRadius = new CornerRadius(0, 37.5, 37.5, 0);
			}
		}

		public HalfNodeUserControl()
		{
			InitializeComponent();
			UpdateCornerRadius();
			InitializeProperties();
			if (isActive)
			{
				InitializeEvent();
			}
		}

		private void InitializeEvent()
		{
			NodeBorder.MouseEnter += OnHover;
			NodeBorder.MouseLeave += OnUnHover;
		}

		public void InitializeProperties()
		{
			Canvas.SetZIndex(NodeMain, 5);
		}

		private void OnHover(object sender, MouseEventArgs e)
		{
			double scale = 1.2;
			NodeBorder.BorderThickness = new Thickness(4);
			TransformGroup hoverGroup = new TransformGroup();
			hoverGroup.Children.Add(new ScaleTransform(scale, scale));
			hoverGroup.Children.Add(new TranslateTransform(-1 * NodeCanvas.Width * (scale - 1) * 0.5, -1 * NodeCanvas.Height * (scale - 1) * 0.5));
			Canvas.SetZIndex(NodeMain, 10);
			NodeBorder.RenderTransform = hoverGroup;
		}

		private void OnUnHover(object sender, MouseEventArgs e)
		{
			NodeBorder.BorderThickness = new Thickness(2);
			NodeBorder.RenderTransform = new TransformGroup();
			Canvas.SetZIndex(NodeMain, 5);
		}

		public void SetStatus(bool status)
		{
			if (status && !isActive)
			{
				Cursor = Cursors.Arrow;
				NodeBorder.Background = Brushes.White;
				NodeBorder.MouseEnter += OnHover;
				NodeBorder.MouseLeave += OnUnHover;
			}
			else if (!status && isActive)
			{
				Cursor = Cursors.None;
				NodeBorder.Background = Brushes.Gray;
				NodeBorder.MouseEnter -= OnHover;
				NodeBorder.MouseLeave -= OnUnHover;
			}
			isActive = status;
		}
	}
}
