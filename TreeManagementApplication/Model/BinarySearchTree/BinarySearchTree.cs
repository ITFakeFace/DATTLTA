using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TreeManagementApplication.Model.BinaryTree;

namespace TreeManagementApplication.Model.BinarySearchTree
{
	internal class BinarySearchTree<T> where T : IComparable<T>
	{
		public BSNode<T>? root;

		public bool IsEmpty()
		{
			return this.root == null;
		}

		public void Print()
		{
			BinarySearchTree<int> bstree = new BinarySearchTree<int>();
			bstree.InsertNode(5);
			bstree.InsertNode(3000);
			bstree.InsertNode(3);
			bstree.InsertNode(4);
			bstree.InsertNode(2000);
			bstree.InsertNode(1);

			bstree.root!.Print();
		}

		public void InsertNode(T value)
		{
			if (this.root == null)
			{
				this.root = new BSNode<T>(value);
			}
			else
			{
				this.root.InsertNode(value);
			}
		}

		public BSNode<T>? FindNode(BSNode<T>? node, T value)
		{
			BSNode<T>? result = null;
			if (node == null)
			{
				return node;
			}

			if (node.value!.CompareTo(value) == 0)
			{
				result = node;
			}
			else if (node.value.CompareTo(value) < 0)
			{
				result = FindNode(node.lNode, value);
			}
			else
			{
				result = FindNode(node.rNode, value);
			}
			return result;
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
<<<<<<< Updated upstream
=======

		public void PrintLNR(INode<T>? node)
		{
			if (node == null) { return; }

			PrintLNR(node.getLNode());
			Console.Write(node.getValue()!.ToString() + "  ");
			PrintLNR(node.getRNode());
		}

		public void PrintLRN(INode<T>? node)
		{
			if (node == null) { return; }

			PrintLNR(node.getLNode());
			PrintLNR(node.getRNode());
			Console.Write(node.getValue()!.ToString() + "  ");
		}

		public void PrintNLR(INode<T>? node)
		{
			if (node == null) { return; }

			Console.Write(node.getValue()!.ToString() + "  ");
			PrintLNR(node.getLNode());
			PrintLNR(node.getRNode());
		}

		public void PrintNRL(INode<T>? node)
		{
			if (node == null) { return; }

			Console.Write(node.getValue()!.ToString() + "  ");
			PrintLNR(node.getRNode());
			PrintLNR(node.getLNode());
		}

		public void PrintRLN(INode<T>? node)
		{
			if (node == null) { return; }

			PrintLNR(node.getRNode());
			PrintLNR(node.getLNode());
			Console.Write(node.getValue()!.ToString() + "  ");
		}

		public void PrintRNL(INode<T>? node)
		{
			if (node == null) { return; }

			PrintLNR(node.getRNode());
			Console.Write(node.getValue()!.ToString() + "  ");
			PrintLNR(node.getLNode());
		}

		public void UpdateNode(T value)
		{
			throw new NotImplementedException();
		}

		public void RemoveNode(T value)
		{
			throw new NotImplementedException();
		}

		public INode<T> findNode(T value)
		{
			throw new NotImplementedException();
		}
		public void deleteNode(T value) 
		{
			
		}
>>>>>>> Stashed changes
	}
}
