using System.CodeDom;
using System.Text;
using TreeManagementApplication.Model.BinarySearchTree;
using TreeManagementApplication.Model.BinaryTree;
using TreeManagementApplication.Model.FileHandle;
using TreeManagementApplication.Model.Interface;

namespace TreeManagementApplication
{
    internal class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            var app = new App();
            AVLTree<int> tree = new AVLTree<int>();
            tree.InsertNode(2);
            tree.InsertNode(3);
            tree.InsertNode(4);
            tree.InsertNode(1);
            tree.InsertNode(5);
            tree.InsertNode(8);
            Console.WriteLine($"NLR: {tree.PrintNLR(tree.GetRoot())}");
            app.InitializeComponent();
            app.Run();
        }
    }
}
