using TreeManagementApplication.Model.BinaryTree;
using TreeManagementApplication.Model.Interface;

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

        public bool UpdateNode(INode<T> node, T value)
        {
            for (int i = 0; i < this.Values!.Count; i++)
            {
                if (Values![i].CompareTo(node.GetValue()) == 0 && Values![i].CompareTo(value) != 0)
                {
                    Values![i] = value;
                    return true;
                }
            }
            node.SetValue(value);
            return false;
        }

        public INode<T>? FindAndDeleteNode(T? value, INode<T>? node = null)
        {
            if (node is null)
            {// Tìm node có giá trị value 
                INode<T>? nodeToDelete = FindNode(value);

                if (nodeToDelete != null)
                {
                    // Xóa node và trả về node đã xóa 
                    Root = DeleteNode(Root, value);
                    return nodeToDelete;
                }
            }
            else
            {
                if (node.GetLNode() is null)
                {

                }
            }

            // Trả về null nếu không tìm thấy node 
            return null;
        }
        private BSNode<T>? DeleteNode(BSNode<T>? root, T value)
        {
            if (root == null)
                return null;

            int compare = value.CompareTo(root.Value);

            if (compare < 0)
            {
                root.LNode = DeleteNode(root.LNode, value);
            }
            else if (compare > 0)
            {
                root.RNode = DeleteNode(root.RNode, value);
            }
            else
            {
                // Node found with value equals 'value' 

                // Case 1: No child or only one child 
                if (root.LNode == null)
                {
                    return root.RNode;
                }
                else if (root.RNode == null)
                {
                    return root.LNode;
                }

                // Case 2: Node with two children 
                // Get the inorder successor (smallest in the right subtree) 
                root.Value = MinValue(root.RNode);

                // Delete the inorder successor 
                root.RNode = DeleteNode(root.RNode, root.Value);
            }

            return root;
        }

        private T MinValue(BSNode<T> node)
        {
            T minValue = node.Value;
            while (node.LNode != null)
            {
                minValue = node.LNode.Value;
                node = node.LNode;
            }
            return minValue;
        }

        public INode<T>? FindNode(T Value)
        {
            if (this.GetRoot() == null) { return null; }
            return this.GetRoot()!.FindChildNode(this.GetRoot()!, Value);
        }
        public INode<T>? FindChildNode(INode<T>? node, T value)
        {
            if (node == null) { return node; }
            int isEqual = node.GetValue()!.CompareTo(value);

            if (isEqual == 0)
            {
                return node;
            }
            else if (isEqual > 0)
            {
                return FindChildNode(node.GetRNode(), value);
            }
            else
            {
                return FindChildNode(node.GetLNode(), value);
            }

        }
        public INode<T>? FindNode(int XIndex, int Level)
        {
            return Root!.FindNode(XIndex, Level);
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
