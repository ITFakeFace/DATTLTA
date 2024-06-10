using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TreeManagementApplication.Model.VisualModel;

namespace TreeManagementApplication.UserControls
{
    public partial class CreateNodeUC : UserControl
    {
        public bool HasLeft { get; set; }

        public bool HasRight { get; set; }


        public CreateNodeUC()
        {
            InitializeComponent();
            InititalizeProperties();
            InitializeEvents();
        }

        public void InititalizeProperties()
        {
            Canvas.SetZIndex(HalfLeft, 5);
            Canvas.SetZIndex(HalfRight, 5);
            HasLeft = false;
            HasRight = false;
        }

        public void InitializeEvents()
        {
            HalfLeft.MouseDown += OnMouseDown;
            HalfRight.MouseDown += OnMouseDown;
        }

        private void OnHover(object sender, MouseEventArgs e)
        {

        }

        private void OnMouseDown(object sender, MouseEventArgs e)
        {
            if (sender.Equals(HalfLeft))
            {
                Console.WriteLine("Left");
                Console.WriteLine($"{Canvas.GetLeft(this)},{Canvas.GetTop(this)}");
                Console.WriteLine($"{NodeGUI<int>.Calculator.GetGridCoordinate(Canvas.GetLeft(this), Canvas.GetTop(this))}");
                GridCoordinate currentIndex = NodeGUI<int>.Calculator.GetGridCoordinate(Canvas.GetLeft(this), Canvas.GetTop(this));
            }
            else if (sender.Equals(HalfRight))
            {
                Console.WriteLine("Right");
                Console.WriteLine($"{Canvas.GetLeft(this)},{Canvas.GetTop(this)}");
                Console.WriteLine($"{NodeGUI<int>.Calculator.GetGridCoordinate(Canvas.GetLeft(this), Canvas.GetTop(this))}");
                GridCoordinate currentIndex = NodeGUI<int>.Calculator.GetGridCoordinate(Canvas.GetLeft(this), Canvas.GetTop(this));
            }
        }
        public void GenerateHalfNode()
        {
            if (HasLeft)
            {
                HalfLeft.SetStatus(false);
            }
            else
            {
                HalfLeft.SetStatus(true);
            }

            if (HasRight)
            {
                HalfRight.SetStatus(false);
            }
            else
            {
                HalfRight.SetStatus(true);
            }
        }
    }
}
