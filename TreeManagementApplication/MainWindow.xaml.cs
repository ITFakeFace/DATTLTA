﻿
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using TreeManagementApplication.Model;
using TreeManagementApplication.Model.BinarySearchTree;
using TreeManagementApplication.Model.BinaryTree;
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
		BinaryTree<int> binaryTree = new BinaryTree<int>();
		int GridSize;

		public MainWindow()
		{
			InitializeComponent();
			GridSize = 75;
			coordinateCalculator = new CoordinateCalculator(new Coordinate(1500, 800), GridSize);
			NodeGUI<int>.Calculator = coordinateCalculator;
		}

		public void InitialConfig()
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

		private void CreateNode(object sender)
		{
			try
			{
				String inp = ValAddInp.Text.Replace(" ", "");
				List<String> inpList = inp.Split(",").ToList();
				foreach (var item in inpList)
				{
					int nodeVal = int.Parse(item);
					//binaryTree.InsertNode(nodeVal);
					binaryTree = (BinaryTree<int>)binaryTree.GenerateRandomTree(10, 1, 10);
					Console.WriteLine("Inserted Node");
				}

				binaryTree.GenerateGridIndex();
				NodeCanvas.Width = (binaryTree.GetLargestX(binaryTree.Root!) + 1) * GridSize;
				Console.WriteLine($"Total NodeCanvas Size: {binaryTree.GetLargestX(binaryTree.Root!) + 1}");
				TreeGUI<int> treeGUI = new TreeGUI<int>();
				treeGUI.DrawTree(binaryTree.Root!, ref NodeCanvas, coordinateCalculator);
				Canvas.SetLeft(NodeCanvas, (canvas.Width - NodeCanvas.Width) / 2);
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
	}
}