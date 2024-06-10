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
        private double _zoom = 0.1;
        private bool _isUpdatingZoom = false;
        private Point _cursorPrevPos;
        private bool _isRightMousePressed = false;
        public static Dictionary<ToolBarMode, ToolBarItemUC> ModeMap;
        public static ToolBarMode BeforeMode { get; set; }
        CoordinateCalculator coordinateCalculator;
        public static ITree<int> Tree = new AVLTree<int>();
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
                    { ToolBarMode.Export, ModeSave },
                    { ToolBarMode.Import, ModeImport },
                    { ToolBarMode.Search, ModeSearch },
                    { ToolBarMode.Traverse, ModeTraverse},
                    { ToolBarMode.ChangeTreeType, ModeChangeTree},
                    //{ ToolBarMode.Select, ModeSelect },
                    };
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
                    index = 6;
                    break;
                case ToolBarMode.Import:
                    index = 7;
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