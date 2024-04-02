
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using TreeManagementApplication.Model;
using TreeManagementApplication.Model.GUI;
using TreeManagementApplication.Model.VisualModel;

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
			coordinateCalculator = new CoordinateCalculator(new Coordinate(1500, 800), 50);
		}

		public void InitialConfig()
		{

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
			/*
			
			*/
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{

		}

		private void Frame_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
		{

		}

		private void Button_Click_1(object sender, RoutedEventArgs e)
		{

		}

		private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			if (SettingMenu.Visibility == Visibility.Visible)
			{
				SettingMenu.Visibility = Visibility.Hidden;
			}
			else if (SettingMenu.Visibility == Visibility.Hidden)
			{
				SettingMenu.Visibility = Visibility.Visible;
			}
		}

		private void CreateNodeBtn_Click_1(object sender, RoutedEventArgs e)
		{
			int row = int.Parse(RowInp.Text);
			int col = int.Parse(ColInp.Text);

			Ellipse ellipse = new Ellipse
			{
				Width = 50,
				Height = 50,
				Fill = Brushes.Red,
				Stroke = Brushes.Black,
				StrokeThickness = 2,

			};

			// Set the position of Child = Mouse Position
			//Canvas.SetLeft(ellipse, coordinateCalculator.gridCoordinateMap[col][row].X - ellipse.Width / 2);
			//Canvas.SetTop(ellipse, coordinateCalculator.gridCoordinateMap[col][row].Y - ellipse.Height / 2);
			Canvas.SetLeft(ellipse, coordinateCalculator.gridCoordinateMap[col][row].X);
			Canvas.SetTop(ellipse, coordinateCalculator.gridCoordinateMap[col][row].Y);

			// Add Child
			canvas.Children.Add(ellipse);
			Console.WriteLine("Node Created");
		}
	}
}