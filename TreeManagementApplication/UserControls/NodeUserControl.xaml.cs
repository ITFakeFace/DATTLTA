using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
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
using static System.Net.Mime.MediaTypeNames;

namespace TreeManagementApplication.UserControls
{
	/// <summary>
	/// Interaction logic for NodeUserControl.xaml
	/// </summary>
	public partial class NodeUserControl : UserControl
	{
		public NodeUserControl()
		{
			InitializeComponent();
			InitializeEvents();
			InitializeProperties();
		}

		public void InitializeProperties()
		{
			Width = 75;
			Height = 75;

			NodeValue.HorizontalAlignment = HorizontalAlignment.Center;
			NodeValue.VerticalAlignment = VerticalAlignment.Center;
			NodeValue.FontSize = 24;

			NodeShape.Height = Height;
			NodeShape.Width = Width;

			Node.Height = Height;
			Node.Width = Width;
		}

		public void InitializeEvents()
		{
			Node.MouseEnter += OnHover;
			Node.MouseLeave += OnUnHover;

		}

		public void OnHover(Object sender, MouseEventArgs e)
		{
			NodeShape.Fill = Brushes.Yellow;
			NodeShape.StrokeThickness = 4;
		}

		public void OnUnHover(Object sender, MouseEventArgs e)
		{
			NodeShape.Fill = Brushes.White;
			NodeShape.StrokeThickness = 2;
		}

		public void SetText(String value)
		{
			NodeValue!.Text = value;
			Size size = MeasureString(NodeValue);
			NodeValue.RenderTransform = new TranslateTransform(-0.5 * size.Width, -0.5 * size.Height);
		}

		public void BalanceTextPosition()
		{
			Size size = MeasureString(NodeValue);
			NodeValue.RenderTransform = new TranslateTransform(-0.5 * size.Width, -0.5 * size.Height);
		}

		public void SetSize(Size size)
		{
			if (size.Width != size.Height)
			{
				Console.WriteLine("Size must be circle");
				return;
			}

			NodeShape!.Width = size.Width;
			NodeShape.Height = size.Height;
			BalanceTextPosition();
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
