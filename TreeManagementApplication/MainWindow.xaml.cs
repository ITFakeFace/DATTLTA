using MaterialDesignColors.Recommended;
using MaterialDesignThemes.Wpf;
using Microsoft.Win32;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Shapes;
using TreeManagementApplication.Model.BinarySearchTree;
using TreeManagementApplication.Model.BinaryTree;
using TreeManagementApplication.Model.FileHandle;
using TreeManagementApplication.Model.GUI;
using TreeManagementApplication.Model.Interface;
using TreeManagementApplication.Model.VisualModel;
using TreeManagementApplication.UserControls;
using TreeManagementApplication.Windows;

namespace TreeManagementApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private double _zoom = 0.1;
        private bool _isUpdatingZoom = false;
        private Point _cursorPrevPos;
        private bool _isRightMousePressed = false;
        public static Dictionary<ToolBarMode, ToolBarItemUC> ModeMap;
        public static ToolBarMode BeforeMode { get; set; }
        CoordinateCalculator coordinateCalculator;
        public static ITree<int> Tree = new BinarySearchTree<int>();
        FileHandler<int> fileHandler = new FileHandler<int>();
        static int SaveAsBinFileMouseCount = 0;
        int GridSize;
        String CurrentTreeType = "Binary Search Tree";
        public MainWindow()
        {
            InitializeComponent();
            InitializeProperties();
            InitializeEvents();
            /*
            Tree.SetRoot(fileHandler.loadBinFile());
            if (!(Tree.GetRoot() == null))
            {
                RerenderTree();
            }
            */

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
                { ToolBarMode.Export, ModeSave },
                { ToolBarMode.Search, ModeSearch },
                { ToolBarMode.Import, ModeImport },
                { ToolBarMode.Traverse, ModeTraverse},
                { ToolBarMode.ChangeTreeType, ModeChangeTree},
            };

            if (Tree.GetType() == typeof(BinaryTree<int>))
            {
                RadioBinaryTree.IsChecked = true;
            }
            else if (Tree.GetType() == typeof(BinarySearchTree<int>))
            {
                RadioBSTree.IsChecked = true;
            }
            else if (Tree.GetType() == typeof(AVLTree<int>))
            {
                RadioAVLTree.IsChecked = true;
            }
        }
        private void InitializeEvents()
        {
            foreach (ToolBarItemUC item in ModeMap.Values)
            {
                item.OnModeChange += OnModeChange;
            }

            //zoom
            MouseWheel += OnMouseWheelEvent;
            //move
            PreviewMouseDown += OnMouseEvent;
            PreviewMouseUp += OnMouseEvent;
            MouseMove += OnMouseMoveEvent;
            canvas.Loaded += InkCanvas_Loaded;
            KeyDown += OnKeyDown;
            KeyUp += OnKeyUp;

            RadioBinaryTree.Checked += OnChangeTreeType;
            RadioBSTree.Checked += OnChangeTreeType;
            RadioAVLTree.Checked += OnChangeTreeType;
        }

        public static ToolBarMode GetCurrentMode()
        {
            foreach (ToolBarMode key in ModeMap.Keys)
            {
                if (ModeMap[key].isActive)
                {
                    return key;
                }
            }
            return ToolBarMode.None;
        }

        void OnKeyDown(object sender, KeyEventArgs e)
        {
            /*
			if ((e.Key == Key.LeftCtrl || e.Key == Key.RightCtrl) && FocusManager.GetFocusedElement(this) == null)
			{
				if (BeforeMode == ToolBarMode.None)
				{
					BeforeMode = GetCurrentMode();
				}
				ToolBarItemUC.DisableAll();
				ModeMove.Enable();
				ChangeMode();
			}
			*/
            /*
			switch (e.Key)
			{
				default:
					break;
			}
			*/
        }


        void OnKeyUp(object sender, KeyEventArgs e)
        {
            /*
			if ((e.Key == Key.LeftCtrl || e.Key == Key.RightCtrl))
			{
				ToolBarItemUC.DisableAll();
				if (BeforeMode != ToolBarMode.None)
				{
					ModeMap[BeforeMode].Enable();
				}
				BeforeMode = ToolBarMode.None;
				ChangeMode();
				Console.WriteLine("Up");
			}
			*/
            if (FocusManager.GetFocusedElement(this) == null)
            {
                switch (e.Key)
                {
                    case Key.M:
                        ToolBarItemUC.DisableAll();
                        if (GetCurrentMode() != ToolBarMode.Move)
                        {
                            ModeMove.Enable();
                        }
                        ChangeMode();
                        break;
                    default:
                        break;
                }
            }
        }


        void OnMouseWheelEvent(object sender, MouseWheelEventArgs e)
        {
            if (ModeMap[ToolBarMode.Move].isActive)
            {
                var diff = e.Delta > 0 ? 0.1 : -0.1;
                _zoom = Math.Clamp(_zoom + diff, 0.1, 10);

                var pos = e.GetPosition(canvas);
                UpdateZoom(pos);
            }
        }

        private void UpdateZoom(Point pos)
        {
            if (ModeMap[ToolBarMode.Move].isActive)
            {
                _isUpdatingZoom = true;

                var matrix = canvas.RenderTransform.Value;

                var targetWidth = canvas.ActualWidth * _zoom * 10d;
                var targetHeight = canvas.ActualHeight * _zoom * 10d;

                var topLeft = canvas.TranslatePoint(new Point(0, 0), this);
                var bottomRight = canvas.TranslatePoint(new Point(canvas.ActualWidth, canvas.ActualHeight), this);

                var renderWidth = bottomRight.X - topLeft.X;
                var renderHeight = bottomRight.Y - topLeft.Y;

                var scaleX = targetWidth / renderWidth;
                var scaleY = targetHeight / renderHeight;

                matrix.ScaleAtPrepend(scaleX, scaleY, pos.X, pos.Y);
                if (matrix.OffsetX > 0)
                    matrix.Translate(-matrix.OffsetX, 0);
                if (matrix.OffsetY > 0)
                    matrix.Translate(0, -matrix.OffsetY);
                if (matrix.OffsetX + targetWidth < NodeContainerParent.ActualWidth)
                    matrix.Translate(NodeContainerParent.ActualWidth - (matrix.OffsetX + targetWidth), 0);
                if (matrix.OffsetY + targetHeight < NodeContainerParent.ActualHeight)
                    matrix.Translate(0, NodeContainerParent.ActualHeight - (matrix.OffsetY + targetHeight));

                canvas.RenderTransform = new MatrixTransform(matrix);
            }
        }
        private void AddMenu_LostFocus(object sender, RoutedEventArgs e)
        { }

        private void InkCanvas_Loaded(object sender, RoutedEventArgs e)
        {
            var matrix = canvas.RenderTransform.Value;
            matrix.ScaleAtPrepend(1, 1, NodeContainerParent.ActualWidth / 2, NodeContainerParent.ActualHeight / 2);
            canvas.RenderTransform = new MatrixTransform(matrix);
        }

        void OnMouseMoveEvent(object sender, MouseEventArgs e)
        {
            if (_isRightMousePressed && ModeMap[ToolBarMode.Move].isActive)
            {
                var cursorPoint = e.GetPosition(this);
                var vector = cursorPoint - _cursorPrevPos;
                _cursorPrevPos = cursorPoint;
                //canvas.Strokes.Transform(new Matrix(1, 0, 0, 1, vector.X * (0.1 / _zoom), vector.Y * (0.1 / _zoom)), false);
                Canvas.SetLeft(NodeCanvas, Canvas.GetLeft(NodeCanvas) + vector.X);
                Canvas.SetTop(NodeCanvas, Canvas.GetTop(NodeCanvas) + vector.Y);
            }
        }

        private void OnMouseEvent(object sender, MouseButtonEventArgs e)
        {
            if (e.RightButton == MouseButtonState.Pressed && !_isRightMousePressed && ModeMap[ToolBarMode.Move].isActive)
            {
                _isRightMousePressed = true;
                _cursorPrevPos = e.GetPosition(this);
            }
            else if (e.RightButton == MouseButtonState.Released && ModeMap[ToolBarMode.Move].isActive)
            {
                _isRightMousePressed = false;
            }

        }

        private void ChangeMode()
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
            ChangeTypeMenu.Visibility = Visibility.Hidden;
            ChangeNodeMenu.Visibility = Visibility.Hidden;
            ExportMode.Visibility = Visibility.Hidden;
            ImportMode.Visibility = Visibility.Hidden;
            int index = -1;
            switch (currentMode)
            {
                case ToolBarMode.Create:
                    index = 0;
                    AddMenu.Visibility = Visibility.Visible;
                    break;
                case ToolBarMode.Update:
                    ChangeNodeMenu.Visibility = Visibility.Visible;
                    index = 1;
                    break;
                case ToolBarMode.Delete:
                    index = 2;
                    break;
                case ToolBarMode.Search:
                    index = 3;
                    break;
                case ToolBarMode.Traverse:
                    index = 4;
                    break;
                case ToolBarMode.Move:
                    index = -1;
                    break;
                case ToolBarMode.Export:
                    ExportMode.Visibility = Visibility.Visible;
                    index = 6;
                    break;
                case ToolBarMode.Import:
                    index = 7;
                    ImportMode.Visibility = Visibility.Visible;
                    break;
                case ToolBarMode.ChangeTreeType:
                    ChangeTypeMenu.Visibility = Visibility.Visible;
                    index = 8;
                    break;
                case ToolBarMode.None:
                default:
                    index = -1;
                    break;
            }
            if (index >= 0)
            {
                ToolBarMenu.Visibility = Visibility.Visible;
                Canvas.SetLeft(ToolBarCursor, 275 + 95 * index);
            }
            else
            {
                ToolBarMenu.Visibility = Visibility.Hidden;
            }
        }

        public void OnModeChange(object sender, EventArgs e)
        {
            ChangeMode();
        }

        private void NodeCanvas_MouseDown(object sender, MouseButtonEventArgs e)
        {

            System.Windows.Point mousePosition = e.GetPosition((UIElement)sender);
            Coordinate coordinate = new Coordinate(mousePosition.X, mousePosition.Y);
            GridCoordinate gridCoordinate = coordinateCalculator!.GetGridCoordinate(coordinate);
            if (ModeMap[ToolBarMode.Update].isActive)
            {

                INode<int>? node = Tree.FindNode(gridCoordinate.X, gridCoordinate.Y);
                if (node != null)
                {

                    BeforeChangeField.Text = node.GetValue().ToString();
                    AfterChangeField.Focus();
                }

            }

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
                    if (CreateNode())
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

        private bool CreateNode()
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
                AddField.Text = "";
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
            if (Tree.GetRoot() == null)
            {
                NodeCanvas.Children.Clear();
                return;
            }
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

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            CreateNode();
        }

        private void TempBtn_Click(object sender, RoutedEventArgs e)
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
        */
        #region before changeNodeVal
        private void BeforeChangeField_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Enter)
                {
                    int inpBeforeChange = int.Parse(BeforeChangeField.Text.ToString());
                    AfterChangeField.Focus();
                }
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine("Argument Null Exception In AddField");
            }
            catch (FormatException ex)
            {
                Console.WriteLine("Input Format Exception In AddField");
            }
            catch (OverflowException ex)
            {
                Console.WriteLine("Out of limit Exception In AddField");
            }
        }



        private INode<int>? FindNode(string inp)
        {
            try
            {
                inp = BeforeChangeField.Text.Replace(" ", "");
                int nodeVal = int.Parse(inp);
                return Tree.FindNode(nodeVal);

            }
            catch (Exception ex)
            {
                if (ex is ArgumentNullException)
                {
                    ErrorWindow error = new ErrorWindow("Arguement can not be null");
                }
                else if (ex is FormatException)
                {

                    ErrorWindow error = new ErrorWindow("Input value must be integer");
                }
                return null;
            }

        }

        private void BeforeChangeField_LostFocus(object sender, RoutedEventArgs e)
        {
            if (BeforeChangeField.Text is null || BeforeChangeField.Text == "")
            {
                BeforeChangeField.Text = "Before";
            }
        }
        private void BeforeChangeField_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            SolidColorBrush brush = new SolidColorBrush();
            brush.Color = (Color)ColorConverter.ConvertFromString("#00d2ff");
            BtnGenerate.BorderBrush = brush;
            BtnGenerate.BorderThickness = new Thickness(4);

        }

        private void BeforeChangeField_GotFocus(object sender, RoutedEventArgs e)
        {
            SolidColorBrush brush = new SolidColorBrush();
            brush.Color = (Color)ColorConverter.ConvertFromString("#00d2ff");
            BtnGenerate.BorderBrush = brush;
            BtnGenerate.BorderThickness = new Thickness(4);
            if (BeforeChangeField.Text.ToLower().Equals("before"))
            {
                BeforeChangeField.Text = null;
            }
        }

        private async void OnChangeTreeType(object sender, EventArgs e)
        {
            if (RadioBinaryTree.IsChecked ?? false && !CurrentTreeType.ToUpper().Equals("BINARY TREE"))
            {
                BooleanMessageWindow check = new BooleanMessageWindow();
                check.ShowDialog();
                if (await check.GetValue())
                {
                    CurrentTreeType = "Binary Tree";
                    RadioBinaryTree.IsChecked = true;
                    Tree = new BinaryTree<int>();
                    RerenderTree();
                }
            }
            else if (RadioBSTree.IsChecked ?? false && !CurrentTreeType.ToUpper().Equals("BINARY SEARCH TREE"))
            {
                BooleanMessageWindow check = new BooleanMessageWindow();
                check.ShowDialog();
                if (await check.GetValue())
                {
                    CurrentTreeType = "Binary Search Tree";
                    RadioBSTree.IsChecked = true;
                    Tree = new BinarySearchTree<int>();
                    RerenderTree();
                }
            }
            else if (RadioAVLTree.IsChecked ?? false && !CurrentTreeType.ToUpper().Equals("AVL TREE"))
            {
                BooleanMessageWindow check = new BooleanMessageWindow();
                check.ShowDialog();
                if (await check.GetValue())
                {
                    CurrentTreeType = "AVL Tree";
                    RadioAVLTree.IsChecked = true;
                    Tree = new AVLTree<int>();
                    RerenderTree();
                }

            }
        }



        private void BtnBeforeChange_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {

                AfterChangeField.Focus();
            }
        }
        #endregion

        private void ChangeNodeMenu_LostFocus(object sender, RoutedEventArgs e)
        {

        }

        private void BtnBeforeChange_GotFocus(object sender, RoutedEventArgs e)
        {
            SolidColorBrush brush = new SolidColorBrush();
            brush.Color = (Color)ColorConverter.ConvertFromString("#00d2ff");
            BtnBeforeChange.BorderBrush = brush;
            BtnBeforeChange.BorderThickness = new Thickness(4);

        }

        #region after change node

        private void AfterChangeField_GotFocus(object sender, RoutedEventArgs e)
        {
            if (AfterChangeField.Text.ToUpper().Equals("AFTER"))
            {
                AfterChangeField.Text = "";
            }
            SolidColorBrush brush = new SolidColorBrush();
            brush.Color = (Color)ColorConverter.ConvertFromString("#00d2ff");
            AddField.BorderThickness = BtnAdd.BorderThickness = new Thickness(4);
            AddField.BorderBrush = BtnAdd.BorderBrush = brush;
        }

        private void AfterChangeField_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                string inpBefore = BeforeChangeField.Text.ToString();
                INode<int>? node = FindNode(inpBefore);
                try
                {
                    if (node != null)
                    {
                        if (AfterChangeField.Text != "")
                        {
                            int inpAftter = int.Parse(AfterChangeField.Text.ToString());
                            Tree.UpdateNode(node, inpAftter);
                            RerenderTree();

                        }
                    }
                    else
                    {
                        ErrorWindow error = new ErrorWindow("Can not find Node with that values");

                    }
                }
                catch (Exception ex)
                {
                    if (ex is FormatException)
                    {
                        ErrorWindow errorWindow = new ErrorWindow("Please input integer");
                    }
                }
            }
        }

        #endregion



        private void SaveAsTxtBtn_Click_1(object sender, RoutedEventArgs e)
        {
            if (Tree.GetRoot() != null)
            {
                string? result = fileHandler.saveFile(Tree);
                if (result != null)
                {
                    ReadFileResult.Text = result;
                    ReadFileResult.Focus();
                }
            }


        }

        private void ImportFileBtn_Click(object sender, RoutedEventArgs e)
        {
            (Queue<object>?, INode<int>?) result = fileHandler.loadFile();
            Queue<object>? queue = result.Item1;
            INode<int>? node = result.Item2;
            if (queue is not null)
            {
                ImportTree(queue);
            }
            else if (node is not null)
            {
                /* ví dụ node là node của Binary Tree
                   node.GetType().Name = "BSNode`1"
                 */
                string nodeImportType = node.GetType().Name.Split('`')[0].ToUpper();
                if (!Tree.nodeTypetoString().Equals(nodeImportType))
                {
                    if (nodeImportType == "BNODE")
                    {
                        Tree = new BinaryTree<int>();
                        CurrentTreeType = "Binary Tree";
                        RadioBinaryTree.IsChecked = true;
                    }
                    else if (nodeImportType == "BSNODE")
                    {
                        Tree = new BinarySearchTree<int>();
                        CurrentTreeType = "Binary Search Tree";
                        RadioBSTree.IsChecked = true;
                    }
                    else
                    {
                        Tree = new AVLTree<int>();
                        CurrentTreeType = "AVL Tree";
                        RadioAVLTree.IsChecked = true;
                    }
                }
                Tree.SetRoot(node);
                RerenderTree();
                return;
            }
            else
            {

                return;
            }

        }

        private void ImportTree(Queue<object>? queue)
        {

            /* đọc phần tử đầu tiên của hàng đợi:
            1: AVL
            2:BinaryTree
            3:BinarySearchTree

            Tree.nodeTypetoString() trả về giá trị  AVLNode tương ứng với cây   AVLTree
                                                    BNode                       BTree
                                                    BSNode                      BNode
             */

            if (queue.Dequeue().ToString().Equals("1") && !Tree.nodeTypetoString().Equals("AVLNODE"))
            {
                Tree = new AVLTree<int>();
                CurrentTreeType = "AVL Tree";
                RadioAVLTree.IsChecked = true;

            }
            else if (queue.Dequeue().ToString().Equals("2") && !CurrentTreeType.ToUpper().Equals("BNODE"))
            {
                Tree = new BinaryTree<int>();
                CurrentTreeType = "Binary Tree";
                RadioBinaryTree.IsChecked = true;
            }
            else if (queue.Dequeue().ToString().Equals("3") && !CurrentTreeType.ToUpper().Equals("BSNODE"))
            {
                Tree = new BinarySearchTree<int>();
                CurrentTreeType = "Binary Search Tree";
                RadioBSTree.IsChecked = true;
            }
            int _status = Tree.Deserialize(queue);
            if (_status == 201)
            {
                ErrorWindow error = new ErrorWindow("Error has occurred when serialize Tree");
                error.Show();
                RerenderTree();
            }
        }

        private void ReadFileResult_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ReadFileResult.SelectAll();

        }



        private void ImportByText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && ModeMap[ToolBarMode.Import].isActive)
            {
                if (!(ImportByText.Text == ""))
                {
                    Queue<object> queue = new Queue<object>();
                    string[] strings = ImportByText.Text.ToString().TrimEnd(',').Split(',');
                    foreach (var item in strings)
                    {
                        queue.Enqueue(item);
                    }
                    ImportTree(queue);
                    RerenderTree();
                }
            }
        }

        private void ImportByText_GotFocus(object sender, RoutedEventArgs e)
        {
            if (ImportByText.Text.ToUpper().Equals("TextFile Import"))
            {
                ImportByText.Text = "";
            }
        }

        private void BtnBeforeChange_Click(object sender, RoutedEventArgs e)
        {
            AfterChangeField.Focus();
        }

        private void BtnAfter_Click(object sender, RoutedEventArgs e)
        {
            if (AfterChangeField.Text.ToLower() != "after")
            {

                string inpBefore = BeforeChangeField.Text.ToString();
                INode<int>? node = FindNode(inpBefore);
                if (node != null)
                {
                    int inpAftter = int.Parse(AfterChangeField.Text.ToString());
                    Tree.UpdateNode(node, inpAftter);
                    RerenderTree();
                }
                else
                {
                    ErrorWindow error = new ErrorWindow("Can not find Node with that values");

                }
            }
        }
    }
}