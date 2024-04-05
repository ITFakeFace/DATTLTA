using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TreeManagementApplication.Model.BinaryTree;
using TreeManagementApplication.Model.Interface;

namespace TreeManagementApplication.Model.BinarySearchTree
{
	internal class BinarySearchTree<T> : ITree<T> where T : IComparable<T>
	{
		public BSNode<T>? root;

		public List<T>? values = new List<T>();

		public bool IsEmpty()
		{
			return this.root == null;
		}

		public void Print()
		{
			ConsoleBinaryTreePrinter<T> printer = new ConsoleBinaryTreePrinter<T>();
			printer.Print(root);
		}

		public void InsertNode(T value)
		{
			if (this.root == null)
			{
				this.root = new BSNode<T>(value);
				values!.Add(value);
			}
			else
			{
				if (this.root.InsertNode(value))
				{
					values!.Add(value);
				}
			}
		}

		public BSNode<T>? FindNode(BSNode<T>? node, T value)
		{
			BSNode<T>? result = null;
			if (node == null)
				return node;

			if (node.value!.CompareTo(value) == 0)
				result = node;
			else if (node.value.CompareTo(value) < 0)
				result = FindNode(node.lNode, value);
			else
				result = FindNode(node.rNode, value);

			return result;
		}

		public void PrintNode(BNode<T>? node, int space)
		{
			if (node == null)
				return;

			PrintNode(node.lNode, space + 1);
			string blankSpace = "";
			for (int i = 0; i < space * 4; i++)
				blankSpace += " ";

			Console.WriteLine(blankSpace + node.value);
			PrintNode(node.rNode, space + 1);
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
		// Other methods omitted for brevity...

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



        public BSNode<T>? FindAndDeleteNode(T value)
        {
            // Tìm node có giá trị value
            BSNode<T>? nodeToDelete = FindNode(root, value);

            if (nodeToDelete != null)
            {
                // Xóa node và trả về node đã xóa
                root = DeleteNode(root, value);
                return nodeToDelete;
            }

            // Trả về null nếu không tìm thấy node
            return null;
        }

        private BSNode<T>? FindNode(BSNode<T>? node, T value)
        {
            if (node == null)
                return null;

            int compare = value.CompareTo(node.value);

            if (compare == 0)
                return node;
            else if (compare < 0)
                return FindNode(node.lNode, value);
            else
                return FindNode(node.rNode, value);
        }

        private BSNode<T>? DeleteNode(BSNode<T>? root, T value)
        {
            if (root == null)
                return null;

            int compare = value.CompareTo(root.value);

            if (compare < 0)
            {
                root.lNode = DeleteNode(root.lNode, value);
            }
            else if (compare > 0)
            {
                root.rNode = DeleteNode(root.rNode, value);
            }
            else
            {
                // Node found with value equals 'value'

                // Case 1: No child or only one child
                if (root.lNode == null)
                {
                    return root.rNode;
                }
                else if (root.rNode == null)
                {
                    return root.lNode;
                }

                // Case 2: Node with two children
                // Get the inorder successor (smallest in the right subtree)
                root.value = MinValue(root.rNode);

                // Delete the inorder successor
                root.rNode = DeleteNode(root.rNode, root.value);
            }

            return root;
        }

        private T MinValue(BSNode<T> node)
        {
            T minValue = node.value;
            while (node.lNode != null)
            {
                minValue = node.lNode.value;
                node = node.lNode;
            }
            return minValue;
        }
    }



}