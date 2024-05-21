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

        public void DeleteTree(BSNode<T> root)
        {
            if (root == null) return;

            // Duyệt qua từng nút con và giải phóng bộ nhớ
            DeleteTree(root.LNode);
            DeleteTree(root.RNode);

            // Giải phóng bộ nhớ của nút hiện tại
            root = null;
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
                Console.WriteLine("Node is not root of Binary Tree");
            }
        }
        public bool UpdateNode(INode<T> node, T value)
        {
            try
            {
                node!.setValue(value);
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
        public List<BNode<T>>? FindNode(T value)
        {
            List<BNode<T>>? found = new List<BNode<T>>();
            FindNodeRecursive(Root, value, found);
            return found;
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
        public INode<T>? FindParentNode(INode<T> node)
        {
            int parentLevel = node.GetLevel() - 1;
            int parentXIndex = node.GetXIndex();
            INode<T>? parent = FindNode(parentXIndex - 1, parentLevel);
            if (parent != null)
            {
                return parent;
            }
            else
            {
                parent = FindNode(parentXIndex + 1, parentLevel);
                return parent;
            }
        }

		INode<T>? ITree<T>.FindNode(T Value)
		{
			throw new NotImplementedException();
		}

        ///------------------------------	
        private INode<T>? GetInOrderSuccessor(INode<T>? Node)
        {
            if (Node == null)
            {
                return null;
            }

            while (Node.GetLNode() != null)
            {
                Node = Node.GetLNode();
            }

            return Node;
        }

        public List<T>? values = new List<T>();

        // Các phương thức khác đã được bỏ qua
        public BNode<T>? FindNode(BNode<T>? node, T value)
        {
            if (node == null)
                return null;

            int compare = value.CompareTo(node.Value);

            if (compare == 0)
                return node;
            else if (compare < 0)
                return FindNode(node.LNode, value);
            else
                return FindNode(node.RNode, value);
        }
        public BNode<T>? FindAndDeleteNode(T value)
        {
            // Tìm node có giá trị value
            Console.WriteLine(1);
            BNode<T>? nodeToDelete = FindNode(Root, value);

            if (nodeToDelete != null)
            {
                // Tìm node kế tiếp trong thứ tự tăng dần (node 70)
                BNode<T>? successor = FindInOrderSuccessor(nodeToDelete);
                if (successor != null)
                {
                    // Thay đổi giá trị của node 50 thành giá trị của node 70
                    nodeToDelete.Value = successor.Value;
                    // Xóa node 70 khỏi cây
                    //Root = DeleteNode(Root, successor);


                }

                Console.WriteLine(9);

                return nodeToDelete;
            }

            // Trả về null nếu không tìm thấy node
            return null;
        }


        /*   private BNode<T>? DeleteNode(BNode<T>? root, BNode<T> nodeToDelete)
           {
               if (root == null)
                   return null;

               int compare = nodeToDelete.Value.CompareTo(root.Value);

               if (compare > 0)
               {
                   root.LNode = DeleteNode(root.LNode, nodeToDelete);
               }
               else if (compare < 0)
               {
                   root.RNode = DeleteNode(root.RNode, nodeToDelete);
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
                   BNode<T>? successor = FindInOrderSuccessor(root);
                   root.Value = successor.Value;
                   successor = null;

                   // Delete the inorder successor

                   root.RNode = DeleteNode(root.RNode, successor);
               }

               return root;
           }
        */
        private BNode<T>? FindInOrderSuccessor(BNode<T> node)
        {
            /* BNode<T>? current = node.RNode;
			 while (current != null && current.LNode != null)
			 {
				 current = current.LNode;

			 }
			 return current;
			 */

            node.GetLevel();
            return null;
        }


        private T MinValue(BNode<T> node)
        {
            Console.WriteLine(6);
            T minValue = node.Value;
            while (node.LNode != null)
            {
                minValue = node.LNode.Value;
                node = node.LNode;
            }
            Console.WriteLine(minValue);
            return minValue;
        }



        INode<T> ITree<T>.DeleteNode(T Value)
        {
            throw new NotImplementedException();
        }


    }


}

