
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using TreeManagementApplication.Model;
using TreeManagementApplication.Model.System;

namespace TreeManagementApplication
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		bool createNodeMode = false;
		CoordinateCalculator coordinateCalculator;

		public MainWindow()
		{
			InitializeComponent();
			WindowState = WindowState.Maximized;
			coordinateCalculator = new CoordinateCalculator(new Coordinate(1500, 1000), 50);
		}

		private void OnCanvasClick(object sender, MouseButtonEventArgs e)
		{

		}

		private void canvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{

		}

		private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{

		}

		private void createNodeBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{

		}

		private void createNodeBtn_Click(object sender, RoutedEventArgs e)
		{
			int row = int.Parse(rowInput.Text);
			int col = int.Parse(columnInput.Text);

			Ellipse ellipse = new Ellipse
			{
				Width = 50,
				Height = 50,
				Fill = Brushes.Red,
				Stroke = Brushes.Black,
				StrokeThickness = 2,

			};

			// Set the position of Child = Mouse Position
			Canvas.SetLeft(ellipse, coordinateCalculator.gridCoordinateMap[col][row].X - ellipse.Width / 2);
			Canvas.SetTop(ellipse, coordinateCalculator.gridCoordinateMap[col][row].Y - ellipse.Height / 2);

			// Add Child
			canvas.Children.Add(ellipse);
			Console.WriteLine("Node Created");
			Line line
		}
	}
}