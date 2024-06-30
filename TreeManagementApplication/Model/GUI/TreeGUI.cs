using System.Windows;
using System.Windows.Controls;
using TreeManagementApplication.Model.Interface;
using TreeManagementApplication.Model.VisualModel;

namespace TreeManagementApplication.Model.GUI
{
    internal class TreeGUI<T> where T : IComparable<T>
    {
        NodeGUI<T>? nodeGUI;
        /*    public void Print(Window window)
            {
                Grid grid = new Grid();

            }*/
        public void DrawTree(INode<T> Root, ref Canvas canvas, CoordinateCalculator calculator)
        {
            nodeGUI = new NodeGUI<T>();
            canvas.Children.Clear();
            nodeGUI.DrawNode(Root, ref canvas);
        }
    }
}
