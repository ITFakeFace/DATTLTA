using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TreeManagementApplication.Model.GUI;
using TreeManagementApplication.Model.Interface;

namespace TreeManagementApplication.Model.BinaryTree
{
	internal class BinaryTree<T> : ITree<T> where T : IComparable<T>
	{
		public BNode<T>? Root { get; set; } = null;

		public List<T>? Values { get; set; }

		public TreeGUI<T> GUI { get; set; } = new TreeGUI<T>();

		public bool IsEmpty()
		{
			return Root == null;
		}
		public void InsertNode(T Value)
		{
			if (Root == null)
			{
				Root = new BNode<T>(Value);
				return;
			}
			Root.InsertNode(Root, Value);
		}
		/*
		public void InsertNode(INode<T>? Node, T Value)
		{
			if (Node == null)
			{
				Root = new BNode<T>(Value);
				return;
			}
			Queue<INode<T>> queue = new Queue<INode<T>>();
			queue.Enqueue(Node);

			// Do level order traversal until we find
			// an empty place.
			while (queue.Count != 0)
			{
				Node = queue.Peek();
				queue.Dequeue();

				if (Node.GetLNode() == null)
				{
					Node.SetLNode(new BNode<T>(Value));
					break;
				}
				else
				{
					queue.Enqueue(Node.GetLNode()!);
				}

				if (Node.GetRNode() == null)
				{
					Node.SetRNode(new BNode<T>(Value));
					break;
				}
				else
					queue.Enqueue(Node.GetRNode()!);
			}
		}
		*/
		public void PrintConsole()
		{
			ConsoleBinaryTreePrinter<T> Printer = new ConsoleBinaryTreePrinter<T>();
			Printer.Print(Root);
		}

		/*
		public void PrintConsole2()
		{
			PrintNodeToConsole(Root, 2);
		}

		public void PrintNodeToConsole(BNode<T>? Node, int Space)
		{
			if (Node == null)
				return;

			PrintNodeToConsole(Node.LNode, Space + 1);
			string BlankSpace = "";
			for (int i = 0; i < Space * 4; i++)
			{
				BlankSpace += " ";
			}
			Console.WriteLine(BlankSpace + Node.Value);
			PrintNodeToConsole(Node.RNode, Space + 1);
		}
		*/

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

		public bool FindNode(INode<T>? Node, T Value)
		{
			if (Node == null)
			{
				return false;
			}
			if (Node.GetValue()!.CompareTo(Value) == 0)
			{
				return true;
			}
			return FindNode(Node.GetLNode(), Value) || FindNode(Node.GetRNode(), Value);
		}

		public INode<T>? FindNode(T Value)
		{
			if (this.Root == null)
			{
				return null;
			}
			return this.Root.FindChildNode(this.Root, Value);
		}

		public INode<T>? GetRoot()
		{
			return this.Root;
		}

		public void SetRoot(INode<T> Node)
		{
			if (Node is BNode<T>)
			{
				this.Root = (BNode<T>)Node;
			}
			else
			{
				Console.WriteLine("Node is not root of Binary Tree");
			}
		}
		public void UpdateNode(T Value)
		{
			throw new NotImplementedException();
		}

		public void RemoveNode(T Value)
		{
			throw new NotImplementedException();
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
