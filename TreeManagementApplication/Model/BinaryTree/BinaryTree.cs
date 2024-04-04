using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TreeManagementApplication.Model.Interface;

namespace TreeManagementApplication.Model.BinaryTree
{
	internal class BinaryTree<T> : ITree<T> where T : IComparable<T>
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
			ConsoleBinaryTreePrinter<T> printer = new ConsoleBinaryTreePrinter<T>();
			printer.Print(root);
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
		public void  UpdateNode(T value)
		{
			
		}


        public void RemoveNode(T value)
		{
			throw new NotImplementedException();
		}
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

        public List<INode<T>>? findNode(T value)
        {
            List<INode<T>>? found = new List<INode<T>>();
            findNodeRecursive(root, value, found);

            return found;
        }

        public void findNodeRecursive(INode<T>? node, T value, List<INode<T>> found)
        {
            if (node == null)
            {
                return;
            }

            int isEqual = node.getValue().CompareTo(value);
            if (isEqual == 0 && !found.Contains(node))
            {
                found.Add(node);
            }

            if (node.getLNode() != null)
            {
                findNodeRecursive(node.getLNode(), value, found);
            }
            if (node.getRNode() != null)
            {
                findNodeRecursive(node.getRNode(), value, found);
            }
        }



    }
}
