using System.Text;
using TreeManagementApplication.Model.BinarySearchTree;
using TreeManagementApplication.Model.BinaryTree;
using TreeManagementApplication.Model.TreeStorage;

namespace TreeManagementApplication
{
	internal class Program
	{
		[STAThread]
		public static void Main(string[] args)
		{
			Console.OutputEncoding = Encoding.UTF8;
			Console.WriteLine("Console is running");
			BinarySearchTree<int> binarySearchTree = new BinarySearchTree<int>();

			Console.WriteLine("");

			binarySearchTree.InsertNode(50);
			binarySearchTree.InsertNode(200);
			binarySearchTree.InsertNode(25);
			binarySearchTree.InsertNode(75);
			binarySearchTree.InsertNode(150);
			binarySearchTree.InsertNode(300);
			binarySearchTree.InsertNode(37);
			binarySearchTree.InsertNode(67);
			binarySearchTree.InsertNode(87);
			binarySearchTree.GenerateGridIndex();
			binarySearchTree.PrintConsole();
			var app = new App();
			app.InitializeComponent();
			app.Run();
		}
	}
}
