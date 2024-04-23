using System.Text;
using TreeManagementApplication.Model.BinaryTree;

namespace TreeManagementApplication
{
	internal class Program
	{
		[STAThread]
		public static void Main(string[] args)
		{
			Console.OutputEncoding = Encoding.UTF8;
			Console.WriteLine("Console is running");
			var app = new App();
			app.InitializeComponent();
			app.Run();
			BinaryTree<int> binaryTree = new BinaryTree<int>();

			Console.WriteLine("");
			/*
			binaryTree.InsertNode(50);
			binaryTree.InsertNode(200);
			binaryTree.InsertNode(25);
			binaryTree.InsertNode(75);
			binaryTree.InsertNode(150);
			binaryTree.InsertNode(300);
			binaryTree.InsertNode(37);
			binaryTree.InsertNode(67);
			binaryTree.InsertNode(87);
			binaryTree.InsertNode(175);
			binaryTree.InsertNode(300);
			binaryTree.InsertNode(70);
			binaryTree.InsertNode(400);
			*/
			//binaryTree.InsertNode(binaryTree.Root!.RNode, 350);
			//binaryTree.GenerateGridIndex();
			//binaryTree.PrintConsole();
		}
	}
}
