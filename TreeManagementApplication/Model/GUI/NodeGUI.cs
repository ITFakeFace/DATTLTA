using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using TreeManagementApplication.Model.Interface;
using TreeManagementApplication.UserControls;

namespace TreeManagementApplication.Model.VisualModel
{
	internal class NodeGUI<T> where T : IComparable<T>
	{
		public static CoordinateCalculator? Calculator { get; set; }
		public static List<NodeUserControl> NodeList { get; set; } = new List<NodeUserControl>();
		public int GridX { get; set; }
		public int GridY { get; set; }
		public double CoorX { get; set; }
		public double CoorY { get; set; }
		public bool isLeftless = false;
		public bool isRightless = false;
		public NodeUserControl node;
		public TextBlock Text { get; set; } = new TextBlock();
		public Coordinate Coordinate { get; set; } = new Coordinate(0, 0);
		public GridCoordinate GridCoordinate { get; set; } = new GridCoordinate(0, 0);
		public NodeGUI()
		{

		}

		public NodeGUI(String text, Coordinate coordinate)
		{
			this.Text.Text = text;
			this.Coordinate = coordinate;
			this.CoorX = coordinate.X;
			this.CoorY = coordinate.Y;
		}
		public void GUI_Click(object sender, MouseEventArgs e)
		{
			NodeUserControl nodeUI = (NodeUserControl)sender;
			Coordinate coordinate = new Coordinate(Canvas.GetLeft(nodeUI), Canvas.GetTop(nodeUI));
			Console.WriteLine(coordinate);
			GridCoordinate gridCoordinate = Calculator!.GetGridCoordinate(coordinate);
			Console.WriteLine(gridCoordinate + "\n");
		}
		public void GUI_Hover(object sender, MouseEventArgs e)
		{
			Ellipse ellipse = (Ellipse)sender;
			ellipse.Fill = Brushes.Yellow;
			ellipse.StrokeThickness = 4;
			Coordinate coordinate = new Coordinate(Canvas.GetLeft(ellipse), Canvas.GetTop(ellipse));
			GridCoordinate gridCoordinate = Calculator!.GetGridCoordinate(coordinate);
		}
		public void GUI_UnHover(object sender, MouseEventArgs e)
		{
			Ellipse ellipse = (Ellipse)sender;
			ellipse.Fill = Brushes.White;
			ellipse.StrokeThickness = 2;
		}

		public void DrawNode(INode<T> Node, ref Canvas canvas)
		{
			if (Node == null)
				return;
			int X = Node.GetXIndex();
			int Y = Node.GetLevel();
			NodeUserControl nodeUI = new NodeUserControl();
			nodeUI.SetText(Node.GetValue() + "");

			Canvas.SetLeft(nodeUI, Calculator!.GetNodeCoordinate(X, Y).X);
			Canvas.SetTop(nodeUI, Calculator.GetNodeCoordinate(X, Y).Y);
			Console.WriteLine($"Coordinate: (value: {Node.GetValue()},X:{Canvas.GetLeft(nodeUI)},Y:{Canvas.GetTop(nodeUI)})");
			Canvas.SetZIndex(nodeUI, 10);
			canvas.Children.Add(nodeUI);
			NodeList.Add(nodeUI);
			if (Node.GetLNode() != null)
			{
				Line LNodeLine = new Line();
				int Y1 = Node.GetLNode()!.GetLevel();
				int X1 = Node.GetLNode()!.GetXIndex();
				LNodeLine.X1 = Calculator.GetNodeCoordinate(X, Y).X + nodeUI.Width / 2;
				LNodeLine.Y1 = Calculator.GetNodeCoordinate(X, Y).Y + nodeUI.Height / 2;
				LNodeLine.X2 = Calculator.GetNodeCoordinate(X1, Y1).X + nodeUI.Width / 2;
				LNodeLine.Y2 = Calculator.GetNodeCoordinate(X1, Y1).Y + nodeUI.Height / 2;
				LNodeLine.Stroke = Brushes.Black;
				LNodeLine.StrokeThickness = 2;
				Canvas.SetZIndex(LNodeLine, 5);
				canvas.Children.Add(LNodeLine);
				DrawNode(Node.GetLNode()!, ref canvas);
			}
			if (Node.GetRNode() != null)
			{
				Line RNodeLine = new Line();
				int Y1 = Node.GetRNode()!.GetLevel();
				int X1 = Node.GetRNode()!.GetXIndex();
				Console.WriteLine(Calculator.GetNodeCoordinate(X, Y));
				Console.WriteLine(nodeUI.Width);
				RNodeLine.X1 = Calculator.GetNodeCoordinate(X, Y).X + nodeUI.Width / 2;
				RNodeLine.Y1 = Calculator.GetNodeCoordinate(X, Y).Y + nodeUI.Height / 2;
				RNodeLine.X2 = Calculator.GetNodeCoordinate(X1, Y1).X + nodeUI.Width / 2;
				RNodeLine.Y2 = Calculator.GetNodeCoordinate(X1, Y1).Y + nodeUI.Height / 2;
				RNodeLine.Stroke = Brushes.Black;
				RNodeLine.StrokeThickness = 2;
				Canvas.SetZIndex(RNodeLine, 5);
				canvas.Children.Add(RNodeLine);
				DrawNode(Node.GetRNode()!, ref canvas);
			}
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
