using System.Diagnostics.Eventing.Reader;
using TreeManagementApplication.Model.BinarySearchTree;
using TreeManagementApplication.Model.Interface;

namespace TreeManagementApplication.Model.BinaryTree
{
    internal class BinaryTree<T> : ITree<T> where T : IComparable<T>
    {
        public BNode<T>? Root { get; set; } = null;

        public List<T>? Values { get; set; } = new List<T>();

        public bool IsEmpty()
        {
            return Root == null;
        }

        public void DeleteTree(INode<T> node)
        {
            // Giải phóng bộ nhớ của nút hiện tại
            node = null;
        }

        public ITree<T> GenerateRandomTree(int Count, int Min, int Max)
        {
            BinaryTree<int> Tree = new BinaryTree<int>();
            if (Count <= 0)
            {
                return null;
            }
            Random rand = new Random();
            Tree.Root = new BNode<int>(rand.Next(Min, Max));
            Count--;
            int tempRandom;
            BNode<int> CurrentNode = Tree.Root;
            while (Count > 0)
            {
                tempRandom = rand.Next(1, 100);
                if (tempRandom <= 50)
                {
                    if (CurrentNode.LNode != null)
                    {
                        CurrentNode = CurrentNode.LNode;
                        continue;
                    }
                    else
                    {
                        CurrentNode.LNode = new BNode<int>(rand.Next(Min, Max));
                        Count--;
                    }
                }
                else
                {
                    if (CurrentNode.RNode != null)
                    {
                        CurrentNode = CurrentNode.RNode;
                        continue;
                    }
                    else
                    {
                        CurrentNode.RNode = new BNode<int>(rand.Next(Min, Max));
                        Count--;
                    }
                }
                CurrentNode = Tree.Root;
            }
            return (ITree<T>)Tree;
        }

        public bool InsertNode(T Value)
        {
            return InsertNode(Root, Value);
        }

        public bool InsertNode(INode<T>? Node, T Value)
        {
            if (Node == null)
            {
                Root = new BNode<T>(Value);
                return true;
            }
            bool result = false;
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
                    result = true;
                    break;
                }
                else
                {
                    queue.Enqueue(Node.GetLNode()!);
                }

                if (Node.GetRNode() == null)
                {
                    Node.SetRNode(new BNode<T>(Value));
                    result = true;
                    break;
                }
                else
                {
                    queue.Enqueue(Node.GetRNode()!);
                }
            }
            return result;
        }

        public void PrintConsole()
        {
            ConsoleBinaryTreePrinter<T> Printer = new ConsoleBinaryTreePrinter<T>();
            Printer.Print(Root);
        }

        /*
=======
		public bool InsertNode(T Value)
		{
			return InsertNode(Root, Value);
		}

		public bool InsertNode(INode<T>? Node, T Value)
		{
			if (Node == null)
			{
				Root = new BNode<T>(Value);
				return true;
			}
			bool result = false;
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
					result = true;
					break;
				}
				else
				{
					queue.Enqueue(Node.GetLNode()!);
				}

				if (Node.GetRNode() == null)
				{
					Node.SetRNode(new BNode<T>(Value));
					result = true;
					break;
				}
				else
				{
					queue.Enqueue(Node.GetRNode()!);
				}
			}
			return result;
		}

		public void PrintConsole()
		{
			ConsoleBinaryTreePrinter<T> Printer = new ConsoleBinaryTreePrinter<T>();
			Printer.Print(Root);
		}

		/*
>>>>>>> Hùng1
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
        public INode<T> FindNode(T value)
        {
            return FindNode(Root, value);

        }
        public INode<T> FindNode(INode<T>? Node, T Value)
        {


            if (Node == null)
            {
                return Node;
            }
            if (Node.GetValue()!.CompareTo(Value) == 0)
            {
                return Node;
            }
            return FindNode(Node.GetLNode(), Value) ?? FindNode(Node.GetRNode(), Value);
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
                Console.WriteLine("Node is not Root of Binary Tree");
            }
        }
        public bool UpdateNode(INode<T> node, T value)
        {
            try
            {
                node!.SetValue(value);
                return true;
            }
            catch
            {
                return false;
            }
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


        public void FindNodeRecursive(BNode<T>? node, T value, List<BNode<T>> found)
        {
            if (node == null)
            {
                return;
            }
            int? isEqual = node.GetValue()?.CompareTo(value);
            if (isEqual == 0 && !found.Contains(node))
            {
                found.Add(node);
            }

            if (node.GetLNode() != null)
                FindNodeRecursive(node.LNode, value, found);
            if (node.GetRNode() != null)
                FindNodeRecursive(node.RNode, value, found);
        }

        public INode<T> DeleteNode(T Value)
        {
            throw new NotImplementedException();
        }

        public List<T>? GetValues()
        {
            return this.Values;
        }
        public INode<T>? FindNode(int XIndex, int Level)
        {
            return Root!.FindNode(XIndex, Level);
        }



        ///------------------------------	


        public INode<T>? FindParentNode(INode<T> node, int XIndex)
        {
            if (node == null)
            {
                return node;
            }

            int isEqual = node.GetXIndex().CompareTo(XIndex);

            if (isEqual < 0)
            {
                INode<T> rNode = node.GetRNode();
                if (rNode != null)
                {
                    bool flag = node.GetRNode()!.GetXIndex().Equals(XIndex);
                    if (flag is true)
                    {
                        return node;
                    }
                    else
                    {
                        return FindParentNode(rNode, XIndex);
                    }
                }
                else return null;
            }

            else if (isEqual > 0)
            {
                INode<T> lNode = node.GetLNode();
                if (lNode != null)
                {
                    bool flag = lNode!.GetXIndex().Equals(XIndex);
                    if (flag is true)
                    {
                        return node;
                    }
                    else
                    {
                        return FindParentNode(lNode, XIndex);
                    }
                }
            }
            return node;
        }


        public void DeleteNode(int XIndex, int Level)
        {
            INode<T> node = FindNode(XIndex, Level);
            DeleteNode(node);

        }
        public void DeleteNode(INode<T> node)
        {
            if (node == null)
            {
                return;
            }
            if (node == Root)
            {
                Root = null!;
                return;
            }
            else
            {
                INode<T> DadNode = FindParentNode(Root, node.GetXIndex());
                if (DadNode.GetLNode() != null)
                {
                    INode<T> DadLnode = DadNode.GetLNode();
                    if (node.GetXIndex() == DadLnode.GetXIndex())
                        DadNode.SetLNode(null);
                }
                if (DadNode.GetRNode() != null)
                {
                    INode<T> DadRnode = DadNode.GetRNode();
                    if (node.GetXIndex() == DadRnode.GetXIndex())
                        DadNode.SetRNode(null);
                }
            }
            return;

        }


        INode<T> ITree<T>.DeleteNode(T Value)
        {
            throw new NotImplementedException();
        }

        public INode<T>? FindParentNode(INode<T> node)
        {
            throw new NotImplementedException();
        }

        public List<string> Serialize()
        {
            throw new NotImplementedException();
        }
        public int GetLargestY(INode<T>? Node)
        {

            if (Node.GetLNode() == null && Node.GetRNode() == null)
            {
                return Node.GetLevel();
            }
            else if (Node.GetLNode() != null && Node.GetRNode() != null)
            {
                if (Node.GetLNode().GetLevel() > Node.GetRNode().GetLevel())
                {
                    return GetLargestY(Node.GetLNode());
                }
                else
                {
                    return GetLargestY(Node.GetRNode());
                }
            }
            else if (Node.GetLNode() != null)
            {
                return GetLargestY(Node.GetLNode());
            }
            else
            {
                return GetLargestY(Node.GetRNode());
            }
        }

        public void Deletenode(INode<T> node)
        {
            throw new NotImplementedException();
        }
    }
}

