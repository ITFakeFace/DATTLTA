using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TreeManagementApplication.Model.BinaryTree
{
	internal class BinaryTree<T> where T : IComparable<T>
	{
		public BNode<T>? root { get; set; } = null;

		public List<T>? values { get; set; }

		public bool IsEmpty()
		{
			return root == null;
		}
		public void InsertNode(T value)
		{
			if (values == null)
			{
				values = new List<T>();
			}
			values.Add(value);
			root = new BNode<T>(values);
		}
		public void Print()
		{
			BinaryTree<int> tree = new BinaryTree<int>();
			tree.InsertNode(5);
			tree.InsertNode(3);
			tree.InsertNode(4);
			tree.InsertNode(2);
			tree.InsertNode(1);
			tree.root!.Print();
		}

		public void Print2()
		{
			PrintNode(root, 2);
		}

		public void PrintNode(BNode<T>? node, int space)
		{
			if (node == null)
				return;

			PrintNode(node.lNode, space + 1);
			string blankSpace = "";
			for (int i = 0; i < space * 4; i++)
			{
				blankSpace += " ";
			}
			Console.WriteLine(blankSpace + node.value);
			PrintNode(node.rNode, space + 1);
		}

		public void TraversePreOrder(BNode<T>? parent)
		{
			if (parent != null)
			{
				Console.Write(parent.value + " ");
				TraversePreOrder(parent.lNode);
				TraversePreOrder(parent.rNode);
			}
		}

		public void PrintLNR(BNode<T>? node)
		{
			if (node == null) { return; }

			PrintLNR(node.lNode);
			Console.Write(node.value!.ToString() + "  ");
			PrintLNR(node.rNode);
		}

		public void PrintLRN(BNode<T>? node)
		{
			if (node == null) { return; }

			PrintLNR(node.lNode);
			PrintLNR(node.rNode);
			Console.Write(node.value!.ToString() + "  ");
		}
	}
}
