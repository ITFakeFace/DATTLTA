using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using TreeManagementApplication.Model.BinarySearchTree;
using TreeManagementApplication.Model.BinaryTree;
using TreeManagementApplication.Model.GUI;
using TreeManagementApplication.Model.Interface;
using TreeManagementApplication.Model.VisualModel;

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
			ToolBarMode? currentMode = ToolBarMode.None;
			foreach (ToolBarMode mode in ModeMap.Keys)
			{
				if (ModeMap[mode].isActive)
				{
					currentMode = mode;
					break;
				}
			}
			AddMenu.Visibility = Visibility.Hidden;
			int index = -1;
			switch (currentMode)
			{
				case ToolBarMode.Create:
					index = 0;
					AddMenu.Visibility = Visibility.Visible;
					break;
				case ToolBarMode.Update:
					index = 1;
					break;
				case ToolBarMode.Delete:
					index = 2;
					break;
				case ToolBarMode.Move:
					index = -1;
					break;
				case ToolBarMode.Save:
					index = 4;
					break;
				case ToolBarMode.Search:
					index = 5;
					break;
				case ToolBarMode.None:
					index = -1;
					break;
				default:
					index = -1;
					break;
			}
			if (index >= 0)
			{
				ToolBarMenu.Visibility = Visibility.Visible;
				Canvas.SetLeft(ToolBarCursor, 417.5 + 95 * index);
			}
			else
			{
				ToolBarMenu.Visibility = Visibility.Hidden;
			}
		}

		private void NodeCanvas_MouseDown(object sender, MouseButtonEventArgs e)
		{
			if (ModeMap[ToolBarMode.Update].isActive)
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
		}

		private void AddField_GotFocus(object sender, RoutedEventArgs e)
		{
			if (AddField.Text.ToUpper().Equals("INSERT"))
			{
				AddField.Text = "";
			}
			SolidColorBrush brush = new SolidColorBrush();
			brush.Color = (Color)ColorConverter.ConvertFromString("#00d2ff");
			AddField.BorderThickness = BtnAdd.BorderThickness = new Thickness(4);
			AddField.BorderBrush = BtnAdd.BorderBrush = brush;
		}

		private void AddMenu_LostFocus(object sender, RoutedEventArgs e)
		{

		}

		private void AddField_LostFocus(object sender, RoutedEventArgs e)
		{
			if (AddField.Text.ToUpper().Equals(""))
			{
				AddField.Text = "Insert";
			}
			SolidColorBrush brush = new SolidColorBrush();
			AddField.BorderThickness = BtnAdd.BorderThickness = new Thickness(2);
			AddField.BorderBrush = BtnAdd.BorderBrush = Brushes.Black;
		}

		private void AddField_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
		{
			if (AddField.Text.ToUpper().Equals("INSERT"))
			{
				AddField.Text = "";
			}
			SolidColorBrush brush = new SolidColorBrush();
			brush.Color = (Color)ColorConverter.ConvertFromString("#00d2ff");
			AddField.BorderThickness = BtnAdd.BorderThickness = new Thickness(4);
			AddField.BorderBrush = BtnAdd.BorderBrush = brush;
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			if (ToolBarMenuCanvas.Height <= 50)
			{
				MenuResizeButtonImg.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/Static/Images/NavBar/Up_100px.png"));
				ToolBarMenuCanvas.Height = 150;
				NavMenyPanel.Visibility = Visibility.Visible;
			}
			else if (ToolBarMenuCanvas.Height > 50)
			{
				MenuResizeButtonImg.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/Static/Images/NavBar/Down_100px.png"));
				ToolBarMenuCanvas.Height = 50;
				NavMenyPanel.Visibility = Visibility.Hidden;
			}
		}

		private void AddField_KeyDown(object sender, KeyEventArgs e)
		{
			switch (e.Key)
			{
				case Key.Enter:
					if (CreateNode(e.Source))
					{
						AddField.Text = "";
					}
					else
					{
						new Thread(() =>
							 {
								 String oldValue = "";
								 this.Dispatcher.Invoke(() =>
												 {
													 oldValue = AddField.Text;
													 AddField.Text = "Error";
												 });
								 Thread.Sleep(2000);
								 this.Dispatcher.Invoke(() =>
													 {
														 if (AddField.Text.Trim().CompareTo("Error") == 0)
														 {
															 AddField.Text = oldValue;
														 }
													 });
							 }).Start();
					}
					break;
				case Key.Tab:
					BtnAdd.Focus();
					break;
				default:
					break;
			}
		}

		private bool CreateNode(object sender)
		{
			try
			{
				String inp = AddField.Text.Replace(" ", "");
				List<String> inpList = inp.Split(",").ToList();
				foreach (var item in inpList)
				{
					int nodeVal = int.Parse(item);
					Tree.InsertNode(nodeVal);
					Console.WriteLine("Inserted Node");
				}
				RerenderTree();
				return true;
			}
			catch (ArgumentNullException ex)
			{
				Console.WriteLine("Argument Null Exception In AddField");
				return false;
			}
			catch (FormatException ex)
			{
				Console.WriteLine("Input Format Exception In AddField");
				return false;
			}
			catch (OverflowException ex)
			{
				Console.WriteLine("Out of limit Exception In AddField");
				return false;
			}
		}
		private void RerenderTree()
		{
			Tree.GenerateGridIndex();
			NodeCanvas.Width = (Tree.GetLargestX(Tree.GetRoot()!) + 1) * GridSize;
			NodeCanvas.Height = (Tree.GetLargestY(Tree.GetRoot()!) + 1) * GridSize;
			Console.WriteLine($"Total NodeCanvas Size: {Tree.GetLargestX(Tree.GetRoot()!) + 1}");
			TreeGUI<int> treeGUI = new TreeGUI<int>();
			treeGUI.DrawTree(Tree.GetRoot()!, ref NodeCanvas, coordinateCalculator);
			Canvas.SetLeft(NodeCanvas, (canvas.ActualWidth - NodeCanvas.ActualWidth) / 2);
			Canvas.SetTop(NodeCanvas, (canvas.ActualHeight - NodeCanvas.ActualHeight) / 2);
		}

		private void BtnGenerate_Click(object sender, RoutedEventArgs e)
		{
			int Count = 0, Min = 0, Max = 0;
			try
			{
				Count = int.Parse(AmountGenField.Text);
			}
			catch (Exception ex)
			{
				Console.WriteLine("Unable to Convert Node Count in Generate Tree Function");
				new Thread(() =>
											 {
												 String oldValue = "";
												 this.Dispatcher.Invoke(() =>
																								 {
																									 oldValue = AddField.Text;
																									 AmountGenField.Text = "Error";
																								 });
												 Thread.Sleep(2000);
												 this.Dispatcher.Invoke(() =>
																									 {
																										 if (AmountGenField.Text.Trim().CompareTo("Error") == 0)
																										 {
																											 AmountGenField.Text = oldValue;
																										 }
																									 });
											 }).Start();
				return;
			}

			try
			{
				Min = int.Parse(MinGenField.Text);
			}
			catch (Exception ex)
			{
				Console.WriteLine("Unable to Convert Node Count in Generate Tree Function");
				new Thread(() =>
														 {
															 String oldValue = "";
															 this.Dispatcher.Invoke(() =>
																														 {
																															 oldValue = AddField.Text;
																															 MinGenField.Text = "Error";
																														 });
															 Thread.Sleep(2000);
															 this.Dispatcher.Invoke(() =>
																															 {
																																 if (AmountGenField.Text.Trim().CompareTo("Error") == 0)
																																 {
																																	 MinGenField.Text = oldValue;
																																 }
																															 });
														 }).Start();
				return;
			}

			try
			{
				Max = int.Parse(MaxGenField.Text);
			}
			catch (Exception ex)
			{
				Console.WriteLine("Unable to Convert Node Count in Generate Tree Function");
				new Thread(() =>
																	 {
																		 String oldValue = "";
																		 this.Dispatcher.Invoke(() =>
																																				 {
																																					 oldValue = AddField.Text;
																																					 MaxGenField.Text = "Error";
																																				 });
																		 Thread.Sleep(2000);
																		 this.Dispatcher.Invoke(() =>
																																					 {
																																						 if (MaxGenField.Text.Trim().CompareTo("Error") == 0)
																																						 {
																																							 MaxGenField.Text = oldValue;
																																						 }
																																					 });
																	 }).Start();
				return;
			}

			Tree = Tree.GenerateRandomTree(Count, Min, Max);
			RerenderTree();
		}

		private void AmountGenField_GotFocus(object sender, RoutedEventArgs e)
		{
			if (AmountGenField.Text.Trim().ToUpper().Equals("AMOUNT"))
			{
				AmountGenField.Text = "";
			}
			SolidColorBrush brush = new SolidColorBrush();
			brush.Color = (Color)ColorConverter.ConvertFromString("#00d2ff");
			BtnGenerate.BorderBrush = brush;
			BtnGenerate.BorderThickness = new Thickness(4);

		}

		private void AmountGenField_LostFocus(object sender, RoutedEventArgs e)
		{
			if (AmountGenField.Text.Trim().ToUpper().Equals(""))
			{
				AmountGenField.Text = "Amount";
			}
			BtnGenerate.BorderBrush = Brushes.Black;
			BtnGenerate.BorderThickness = new Thickness(2);
		}

		private void MinGenField_GotFocus(object sender, RoutedEventArgs e)
		{
			if (MinGenField.Text.Trim().ToUpper().Equals("MIN"))
			{
				MinGenField.Text = "";
			}
			SolidColorBrush brush = new SolidColorBrush();
			brush.Color = (Color)ColorConverter.ConvertFromString("#00d2ff");
			BtnGenerate.BorderBrush = brush;
			BtnGenerate.BorderThickness = new Thickness(4);
		}

		private void MinGenField_LostFocus(object sender, RoutedEventArgs e)
		{
			if (MinGenField.Text.Trim().ToUpper().Equals(""))
			{
				MinGenField.Text = "Min";
			}
			BtnGenerate.BorderBrush = Brushes.Black;
			BtnGenerate.BorderThickness = new Thickness(2);
		}

		private void MaxGenField_GotFocus(object sender, RoutedEventArgs e)
		{
			if (MaxGenField.Text.Trim().ToUpper().Equals("MAX"))
			{
				MaxGenField.Text = "";
			}
			SolidColorBrush brush = new SolidColorBrush();
			brush.Color = (Color)ColorConverter.ConvertFromString("#00d2ff");
			BtnGenerate.BorderBrush = brush;
			BtnGenerate.BorderThickness = new Thickness(4);
		}

		private void MaxGenField_LostFocus(object sender, RoutedEventArgs e)
		{
			if (MaxGenField.Text.Trim().ToUpper().Equals(""))
			{
				MaxGenField.Text = "Max";
			}
			BtnGenerate.BorderBrush = Brushes.Black;
			BtnGenerate.BorderThickness = new Thickness(2);
		}

		private void BtnGenerate_GotFocus(object sender, RoutedEventArgs e)
		{
			SolidColorBrush brush = new SolidColorBrush();
			brush.Color = (Color)ColorConverter.ConvertFromString("#00d2ff");
			BtnGenerate.BorderBrush = brush;
			BtnGenerate.BorderThickness = new Thickness(4);
		}

		private void BtnGenerate_LostFocus(object sender, RoutedEventArgs e)
		{
			BtnGenerate.BorderBrush = Brushes.Black;
			BtnGenerate.BorderThickness = new Thickness(2);
		}

		private void BtnAdd_GotFocus(object sender, RoutedEventArgs e)
		{
			SolidColorBrush brush = new SolidColorBrush();
			brush.Color = (Color)ColorConverter.ConvertFromString("#00d2ff");
			BtnAdd.BorderBrush = brush;
			BtnAdd.BorderThickness = new Thickness(4);
		}

		private void BtnAdd_LostFocus(object sender, RoutedEventArgs e)
		{
			BtnAdd.BorderBrush = Brushes.Black;
			BtnAdd.BorderThickness = new Thickness(2);
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

		   }
		   */

	}
}