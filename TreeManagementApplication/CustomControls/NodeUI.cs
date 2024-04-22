using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace TreeManagementApplication.CustomControls
{
	internal class NodeUI : Control
	{
		private Canvas? nodeCanvas;
		private Ellipse? shape;
		private TextBlock? text;
		static NodeUI()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(NodeUI), new FrameworkPropertyMetadata(typeof(NodeUI)));
		}

		public override void OnApplyTemplate()
		{
			nodeCanvas = Template.FindName("Node", this) as Canvas;
			shape = Template.FindName("Shape", this) as Ellipse;
			text = Template.FindName("Value", this) as TextBlock;
			InitializeProperties();
			InitializeEvents();
			base.OnApplyTemplate();
		}

		public void InitializeProperties()
		{

		}

		public void SetText(String value)
		{
			text!.Text = value;
			Size size = MeasureString(text);
			text.RenderTransform = new TranslateTransform(Width = size.Width, Height = size.Height);
		}

		public void SetSize(Size size)
		{
			if (size.Width != size.Height)
			{
				Console.WriteLine("Size must be circle");
				return;
			}

			shape.Width = size.Width;
			shape.Height = size.Height;
			Canvas.SetTop(text, shape.Height / 2);
			Canvas.SetLeft(text, shape.Width / 2);
		}

		public void InitializeEvents()
		{
			nodeCanvas!.MouseEnter += OnHover;
			nodeCanvas.MouseLeave += OnUnHover;

		}

		public void OnHover(Object sender, MouseEventArgs e)
		{
			this.shape!.Fill = Brushes.Yellow;
			this.shape.StrokeThickness = 4;
		}

		public void OnUnHover(Object sender, MouseEventArgs e)
		{
			this.shape!.Fill = Brushes.White;
			this.shape.StrokeThickness = 2;
		}

		private Size MeasureString(TextBlock candidate)
		{
			var formattedText = new FormattedText(
				candidate.Text,
				CultureInfo.CurrentCulture,
				FlowDirection.LeftToRight,
				new Typeface(candidate.FontFamily, candidate.FontStyle, candidate.FontWeight, candidate.FontStretch),
				candidate.FontSize,
				Brushes.Black,
				new NumberSubstitution(),
				VisualTreeHelper.GetDpi(candidate).PixelsPerDip
			);

			return new Size(formattedText.Width, formattedText.Height);
		}
	}
}
