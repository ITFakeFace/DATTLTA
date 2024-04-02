using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TreeManagementApplication.Model.BinarySearchTree;
using TreeManagementApplication.Model.BinaryTree;

namespace TreeManagementApplication
{
	internal class Program
	{
		[STAThread]
		public static void Main(string[] args)
		{

			var app = new App();
			Console.WriteLine("Console is running");
			app.InitializeComponent();
			app.Run();
			BinaryTree<int> binaryTree = new BinaryTree<int>();

			Console.WriteLine("");
			binaryTree.InsertNode(5);
			binaryTree.InsertNode(40000);
			binaryTree.InsertNode(4);
			binaryTree.InsertNode(2);
			binaryTree.InsertNode(1);
			binaryTree.Print();
		}
	}
}
