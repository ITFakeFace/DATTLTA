using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Xml.Linq;
using TreeManagementApplication.Model.BinaryTree;
using TreeManagementApplication.Model.Interface;
using TreeManagementApplication.Model.VisualModel;

namespace TreeManagementApplication.Model.BinarySearchTree
{
	internal class BinarySearchTree<T> : ITree<T> where T : IComparable<T>
	{
		public BSNode<T>? Root { get; set; }

		public List<T>? Values { get; set; } = new List<T>();

		public bool IsEmpty()
		{
			return this.Root == null;
		}

		public INode<T>? GetRoot()
		{
			return this.GetRoot();
		}

		public void SetRoot(INode<T> Node)
		{
			if (Node is BSNode<T>)
			{
				this.Root = (BSNode<T>)Node;
			}
			else
			{
				Console.WriteLine("Node is not root of Binary Tree");
			}
		}

		public void PrintConsole()
		{
			ConsoleBinaryTreePrinter<T> printer = new ConsoleBinaryTreePrinter<T>();
			printer.Print(Root);
		}

		public void InsertNode(T Value)
		{
			if (this.Root == null)
			{
				this.Root = new BSNode<T>(Value);
				Values!.Add(Value);
			}
			else
			{
				if (this.Root.InsertNode(Value))
				{
					Values!.Add(Value);
				}
			}
		}

		public void PrintNode(BNode<T>? Node, int space)
		{
			if (Node == null)
				return;

			PrintNode(Node.LNode, space + 1);
			string blankSpace = "";
			for (int i = 0; i < space * 4; i++)
				blankSpace += " ";

			Console.WriteLine(blankSpace + Node.Value);
			PrintNode(Node.RNode, space + 1);
		}

		public void PrintLNR(INode<T>? Node)
		{
			if (Node == null) { return; }

			PrintLNR(Node.GetLNode());
			Console.Write(Node.GetValue()!.ToString() + "  ");
			PrintLNR(Node.GetRNode());
		}

		public void PrintLRN(INode<T>? Node)
		{
			if (Node == null) { return; }

			PrintLNR(Node.GetLNode());
			PrintLNR(Node.GetRNode());
			Console.Write(Node.GetValue()!.ToString() + "  ");
		}

		public void PrintNLR(INode<T>? Node)
		{
			if (Node == null) { return; }

			Console.Write(Node.GetValue()!.ToString() + "  ");
			PrintLNR(Node.GetLNode());
			PrintLNR(Node.GetRNode());
		}

		public void PrintNRL(INode<T>? Node)
		{
			if (Node == null) { return; }

			Console.Write(Node.GetValue()!.ToString() + "  ");
			PrintLNR(Node.GetRNode());
			PrintLNR(Node.GetLNode());
		}

		public void PrintRLN(INode<T>? Node)
		{
			if (Node == null) { return; }

			PrintLNR(Node.GetRNode());
			PrintLNR(Node.GetLNode());
			Console.Write(Node.GetValue()!.ToString() + "  ");
		}

		public void PrintRNL(INode<T>? Node)
		{
			if (Node == null) { return; }

			PrintLNR(Node.GetRNode());
			Console.Write(Node.GetValue()!.ToString() + "  ");
			PrintLNR(Node.GetLNode());
		}

		public void UpdateNode(T Value)
		{
			throw new NotImplementedException();
		}

		public void RemoveNode(T Value)
		{
			throw new NotImplementedException();
		}

		public INode<T>? FindNode(T Value)
		{
			if (this.GetRoot() == null) { return null; }
			return this.GetRoot()!.FindChildNode(this.GetRoot()!, Value);
		}


		public void PrintIndexConsole()
		{
			PrintNodeIndexToConsole(Root, 2);
		}

		public void PrintNodeIndexToConsole(BSNode<T>? Node, int Space)
		{
			if (Node == null)
				return;

			PrintNodeIndexToConsole(Node.LNode, Space + 1);
			string BlankSpace = "";
			for (int i = 0; i < Space * 4; i++)
			{
				BlankSpace += " ";
			}
			Console.WriteLine(BlankSpace + $"({Node.Value},{Node.XIndex},{Node.Level})");
			PrintNodeIndexToConsole(Node.RNode, Space + 1);
		}

		public void GenerateGridIndex()
		{
			if (Root == null) { return; }
			int pos = 0;
			Root.CalcX(ref pos);
			pos = 0;
			Root.CalcY(pos);
		}

		public int GetLargestX(INode<T> Node)
		{
			if (Node.GetRNode() == null)
			{
				return Node.GetXIndex();
			}
			return GetLargestX(Node.GetRNode()!);

		}
	}
}
