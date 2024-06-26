using System.Globalization;
using System.Linq;
using System.Printing.Interop;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using TreeManagementApplication.Model.VisualModel;

namespace TreeManagementApplication.UserControls
{
    public partial class NodeUserControl : UserControl
    {
        public CreateNodeUC? CreateNodeUC { get; set; } = new CreateNodeUC();
        public NodeUserControl()
        {
            InitializeComponent();
            InitializeProperties();
            InitializeEvents();
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

            Canvas.SetZIndex(NodeShape, 10);
            Canvas.SetZIndex(NodeValue, 11);
        }

        public void InitializeEvents()
        {
            Node.MouseEnter += OnHover;
            Node.MouseLeave += OnUnHover;
            Node.MouseDown += OnClick;
            CreateNodeUC!.MouseDown += CreateNodeUC_OnClick;
            CreateNodeUC.HalfLeft.MouseDown += HalfLeft_OnClick;
            CreateNodeUC.HalfRight.MouseDown += HalfRight_OnClick;
        }

        public void OnHover(Object sender, MouseEventArgs e)
        {
            if (NodeShape.Fill == Brushes.Red && MainWindow.GetCurrentMode().Equals(ToolBarMode.Update))
            {
                return;
            }
            NodeShape.Fill = Brushes.Yellow;
            NodeShape.StrokeThickness = 4;
            if (CreateNodeUC != null && MainWindow.ModeMap[ToolBarMode.Create].isActive)
            {
                Canvas.SetLeft(CreateNodeUC, 0);
                Canvas.SetTop(CreateNodeUC, NodeShape.Height * 0.5);
                Canvas.SetZIndex(CreateNodeUC, 9);
                if (!Node.Children.Contains(CreateNodeUC))
                {
                    Node.Children.Add(CreateNodeUC);
                }
                CreateNodeUC.Visibility = Visibility.Visible;
            }
        }

        public async void OnUnHover(Object sender, MouseEventArgs e)
        {

            if (NodeShape.Fill == Brushes.Red && MainWindow.GetCurrentMode().Equals(ToolBarMode.Update))
            {
                return;
            }
            NodeShape.Fill = Brushes.White;
            NodeShape.StrokeThickness = 2;
            await Task.Delay(0);
            if (CreateNodeUC != null && !CreateNodeUC.IsMouseOver)
            {
                //CreateNodeUC.Visibility = Visibility.Hidden;
                Node.Children.Remove(CreateNodeUC);
            }
        }

        public void OnClick(Object sender, MouseEventArgs e)
        {
            ToolBarMode mode = MainWindow.GetCurrentMode();
            switch (mode)
            {
                case ToolBarMode.Update:
                    Console.WriteLine("Update");
                    if (NodeShape.Fill != Brushes.Red)
                    {
                        NodeShape.Fill = Brushes.Red;
                    }
                    else
                    {
                        NodeShape.Fill = null;
                    }
                    break;
                case ToolBarMode.Delete:
                    Console.WriteLine("Delete");
                    break;
            }
            Console.WriteLine($"{Canvas.GetLeft(this)},{Canvas.GetTop(this)}");
            Console.WriteLine($"{NodeGUI<int>.Calculator.GetGridCoordinate(Canvas.GetLeft(this), Canvas.GetTop(this))}");
        }

        public void CreateNodeUC_OnClick(object sender, MouseEventArgs e)
        {
            Console.WriteLine("CreateUC Event activated");
            if (sender.Equals(CreateNodeUC!.HalfLeft))
            {

            }
            else if (sender.Equals(CreateNodeUC.HalfRight))
            {

            }
        }
        public void HalfLeft_OnClick(object sender, MouseEventArgs e)
        {
            Console.WriteLine($"Left: {Canvas.GetLeft(this)},{Canvas.GetTop(this)}");
            GridCoordinate currentIndex = NodeGUI<int>.Calculator.GetGridCoordinate(Canvas.GetLeft(this), Canvas.GetTop(this));
        }

        public void HalfRight_OnClick(object sender, MouseEventArgs e)
        {
            Console.WriteLine($"Right: {Canvas.GetLeft(this)},{Canvas.GetTop(this)}");
            GridCoordinate currentIndex = NodeGUI<int>.Calculator.GetGridCoordinate(Canvas.GetLeft(this), Canvas.GetTop(this));
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
