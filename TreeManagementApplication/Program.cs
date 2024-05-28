using System.CodeDom;
using System.Text;
using TreeManagementApplication.Model.BinarySearchTree;
using TreeManagementApplication.Model.BinaryTree;
using TreeManagementApplication.Model.Interface;
using TreeManagementApplication.Model.TreeStorage;

namespace TreeManagementApplication
{
    internal class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            var app = new App();
            app.InitializeComponent();
            app.Run();
        }
    }
}
