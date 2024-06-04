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
            app.InitializeComponent();
            app.Run();
        }
    }
}
