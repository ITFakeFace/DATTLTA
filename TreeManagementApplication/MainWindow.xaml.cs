using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TreeManagementApplication.Model.BinarySearchTree;
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
        CoordinateCalculator coordinateCalculator;
        BinarySearchTree<int> binaryTree = new BinarySearchTree<int>();
        NodeGUI<int> node = new NodeGUI<int>();
        int GridSize;
        TreeGUI<int> treeGUI = new TreeGUI<int>();
        public MainWindow()
        {
            InitializeComponent();
            GridSize = 75;
            coordinateCalculator = new CoordinateCalculator(new Coordinate(1500, 800), GridSize);
            NodeGUI<int>.Calculator = coordinateCalculator;
        }

        public void InitialConfig()
        {
            ValAddInp.GotFocus += ValAddInp_GotFocus;
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
                Console.WriteLine(ValAddInp.Text.ToString());
                String inp = ValAddInp.Text.Replace(" ", "");
                List<String> inpList = inp.Split(",").ToList();
                foreach (var item in inpList)
                {
                    int nodeVal = int.Parse(item);
                    binaryTree.InsertNode(nodeVal);
                    Console.WriteLine("Inserted Node");
                }

                binaryTree.GenerateGridIndex();
                NodeCanvas.Width = (binaryTree.GetLargestX(binaryTree.Root!) + 1) * GridSize;
                Console.WriteLine($"Total NodeCanvas Size: {binaryTree.GetLargestX(binaryTree.Root!) + 1}");
                if (treeGUI == null)
                {
                    treeGUI = new TreeGUI<int>();
                }
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
                Console.WriteLine("Out of limit Exaception");
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


        private void NodeCanvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            System.Windows.Point mousePosition = e.GetPosition((UIElement)sender);
            Coordinate coordinate = new Coordinate(mousePosition.X, mousePosition.Y);
            GridCoordinate gridCoordinate = coordinateCalculator!.GetGridCoordinate(coordinate);
            ChangeNodeWindow changeNodeWindow = new ChangeNodeWindow();
            changeNodeWindow.ShowDialog();
            string changeNodeVal = changeNodeWindow.value.Replace(" ", "").TrimEnd(',');
            int changeNodeNum = int.Parse(changeNodeVal);
            INode<int>? node = binaryTree.FindNode(gridCoordinate.X, gridCoordinate.Y);
            if (!(changeNodeVal == ""))
            {
                if (binaryTree.UpdateNode(node!, changeNodeNum))
                {
                    UpdateTree();
                    treeGUI = null!;
                    CreateNode(sender);
                }
                else
                {
                    Console.WriteLine("Your value may exist in tree");
                }
            }

        }
        public void UpdateTree()
        {
            string? nodes = null;
            int count = binaryTree.Values!.Count();
            for (int i = 0; i < binaryTree.Values!.Count; i++)
            {
                if (i == (binaryTree.Values!.Count - 1))
                {
                    nodes += binaryTree.Values![i];
                }
                else
                {
                    nodes += binaryTree.Values![i] + ",";
                }
            }
            ValAddInp.Text = "";
            ValAddInp.Text = nodes;
            Console.WriteLine(ValAddInp.Text!.ToString());
        }
        public void DeleteNode()
        {

        }







    }
}