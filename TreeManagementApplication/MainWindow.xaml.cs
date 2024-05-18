using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using TreeManagementApplication.Model.BinarySearchTree;
using TreeManagementApplication.Model.BinaryTree;
using TreeManagementApplication.Model.GUI;
using TreeManagementApplication.Model.Interface;
using TreeManagementApplication.Model.VisualModel;
using TreeManagementApplication.UserControls;

namespace TreeManagementApplication
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{

		public static Dictionary<ToolBarMode, ToolBarItemUC> ModeMap;

		CoordinateCalculator coordinateCalculator;
		public static ITree<int> Tree = new BinarySearchTree<int>();
		int GridSize;
		public MainWindow()
		{
			InitializeComponent();
			InitializeProperties();
			InitializeEvents();
		}
		private void InitializeProperties()
		{
			GridSize = 75;
			coordinateCalculator = new CoordinateCalculator(new Coordinate(1500, 800), GridSize);
			NodeGUI<int>.Calculator = coordinateCalculator;
			ModeMap = new Dictionary<ToolBarMode, ToolBarItemUC> {
				{ ToolBarMode.Create, ModeCreate },
				{ ToolBarMode.Update, ModeUpdate },
				{ ToolBarMode.Delete, ModeDelete },
				{ ToolBarMode.Move, ModeMove },
				{ ToolBarMode.Save, ModeSave },
				{ ToolBarMode.Search, ModeSearch },
				//{ ToolBarMode.Load, ModeLoad },
				//{ ToolBarMode.Select, ModeSelect },
			};
		}
		private void InitializeEvents()
		{
			foreach (ToolBarItemUC item in ModeMap.Values)
			{
				item.OnModeChange += OnModeChange;
			}
		}

		public void OnModeChange(object sender, EventArgs e)
		{
			Console.WriteLine("Mode Change");
		}

		private void NodeCanvas_MouseDown(object sender, MouseButtonEventArgs e)
		{
			System.Windows.Point mousePosition = e.GetPosition((UIElement)sender);
			Coordinate coordinate = new Coordinate(mousePosition.X, mousePosition.Y);
			GridCoordinate gridCoordinate = coordinateCalculator!.GetGridCoordinate(coordinate);

			ChangeNodeWindow changeNodeWindow = new ChangeNodeWindow();
			Main.Background = new SolidColorBrush(Color.FromRgb(128, 128, 128));
			changeNodeWindow.ShowDialog();
			String? changeNodeVal = changeNodeWindow.InpValue;
			INode<int>? node = Tree.FindNode(gridCoordinate.X, gridCoordinate.Y);
			if (!(changeNodeVal == null || node == null))
			{
				int nodeNewVal = int.Parse(changeNodeVal.Replace(" ", ""));
				Console.WriteLine("Rerender Tree");
				Tree.UpdateNode(node, nodeNewVal);
				//RerenderTree();
			}
		}

		void CheckModeChange()
		{

		}

		/*
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
			CreateNode(e.Source);
		}

		private void RowInp_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Enter)
			{
				CreateNode(e.Source);
				ValAddInp.Text = "";
			}
		}

		private void CreateNode(object sender)
		{
			try
			{
				String inp = ValAddInp.Text.Replace(" ", "");
				List<String> inpList = inp.Split(",").ToList();
				foreach (var item in inpList)
				{
					int nodeVal = int.Parse(item);
					Tree.InsertNode(nodeVal);
					Console.WriteLine("Inserted Node");
				}
				RerenderTree();
			}
			catch (ArgumentNullException ex)
			{
				Console.WriteLine("Input Cannot Null");
			}
			catch (FormatException ex)
			{
				Console.WriteLine("Input Format Exception");
			}
			catch (OverflowException ex)
			{
				Console.WriteLine("Out of limit Exception");
			}
		}

		private void RerenderTree()
		{
			Tree.GenerateGridIndex();
			NodeCanvas.Width = (Tree.GetLargestX(Tree.GetRoot()!) + 1) * GridSize;
			Console.WriteLine($"Total NodeCanvas Size: {Tree.GetLargestX(Tree.GetRoot()!) + 1}");
			TreeGUI<int> treeGUI = new TreeGUI<int>();
			treeGUI.DrawTree(Tree.GetRoot()!, ref NodeCanvas, coordinateCalculator);
			Canvas.SetLeft(NodeCanvas, (canvas.Width - NodeCanvas.Width) / 2);
		}
		private void ValAddInp_GotFocus(object sender, RoutedEventArgs e)
		{
			if (ValAddInp.Text.ToLower().Equals("value"))
			{
				ValAddInp.Text = "";
			}
		}

		private void RowInp_TextChanged(object sender, TextChangedEventArgs e)
		{

		}

		

		private void NodeCountInp_GotFocus(object sender, RoutedEventArgs e)
		{
			if (NodeCountInp.Text.ToLower().Equals("node count"))
			{
				NodeCountInp.Text = "";
			}
		}

		private void NodeCountInp_LostFocus(object sender, RoutedEventArgs e)
		{
			if (NodeCountInp.Text.Trim().Length == 0)
			{
				NodeCountInp.Text = "Node Count";
			}
		}

		private void MinValInp_GotFocus(object sender, RoutedEventArgs e)
		{
			if (MinValInp.Text.ToLower().Equals("min"))
			{
				MinValInp.Text = "";
			}
		}
		private void MinValInp_LostFocus(object sender, RoutedEventArgs e)
		{
			if (MinValInp.Text.Trim().Length == 0)
			{
				MinValInp.Text = "Min";
			}
		}


		private void MaxValInp_GotFocus(object sender, RoutedEventArgs e)
		{
			if (MaxValInp.Text.ToLower().Equals("max"))
			{
				MaxValInp.Text = "";
			}
		}

		private void MaxValInp_LostFocus(object sender, RoutedEventArgs e)
		{
			if (MaxValInp.Text.Trim().Length == 0)
			{
				MaxValInp.Text = "Max";
			}
		}

		private void NodeCountInp_KeyDown(object sender, KeyEventArgs e)
		{
			e.Handled = !((e.Key >= Key.D0 && e.Key <= Key.D9) || (Keyboard.IsKeyToggled(Key.NumLock) && (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)));
		}

		private void TreeGenerateBtn_Click(object sender, RoutedEventArgs e)
		{
			int Count = 0, Min = 0, Max = 0;
			try
			{
				Count = int.Parse(NodeCountInp.Text);
			}
			catch (Exception ex)
			{
				Console.WriteLine("Unable to Convert Node Count in Generate Tree Function");
			}

			try
			{
				Min = int.Parse(MinValInp.Text);
			}
			catch (Exception ex)
			{
				Console.WriteLine("Unable to Convert Node Count in Generate Tree Function");
			}

			try
			{
				Max = int.Parse(MaxValInp.Text);
			}
			catch (Exception ex)
			{
				Console.WriteLine("Unable to Convert Node Count in Generate Tree Function");
			}

			Tree = ((BinaryTree<int>)Tree).GenerateRandomTree(Count, Min, Max);
			RerenderTree();
		}
		*/
	}
}