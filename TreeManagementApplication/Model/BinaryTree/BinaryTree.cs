using TreeManagementApplication.Model.BinarySearchTree;
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

        public void DeleteTree(BSNode<T> root)
        {
            if (root == null) return;

            // Duyệt qua từng nút con và giải phóng bộ nhớ
            DeleteTree(root.LNode);
            DeleteTree(root.RNode);

            // Giải phóng bộ nhớ của nút hiện tại
            root = null;
        }

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

        public void PrintConsole()
        {
            ConsoleBinaryTreePrinter<T> Printer = new ConsoleBinaryTreePrinter<T>();
            Printer.Print(Root);
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
            }
            found.Add(node);

            {
                if (node.GetLNode() != null)
                    FindNodeRecursive(node.LNode, value, found);
                {
                }
                if (node.GetRNode() != null)
                    FindNodeRecursive(node.RNode, value, found);
            }
        }


        public void editNode(T nodeValue, T valueReplaced)
        {
            if (nodeValue == null) { return; }
            ConsoleBinaryTreePrinter<T> printer = new ConsoleBinaryTreePrinter<T>();
            {
                List<BNode<T>>? found = new List<BNode<T>>();
                found = FindNode(nodeValue);
                if (found == null)
                    return;
                else if (found.Count == 1)
                {
                    found[0].setValue(valueReplaced);
                }
                else
                {
                    for (int i = 0; i < found.Count; i++)
                    {
                        printer.Print(found[i]);
                    }
                    for (int i = 0; i < found.Count; i++)
                    {
                        Console.WriteLine($"Choose node to delete(input -1 to exit)");
                        Console.WriteLine($"{i + 1}: Delete {i + 1} node");
                    }
                    try
                    {
                        int choose = -1;

                        if (choose >= 0 && choose < found.Count)
                            choose = int.Parse(s: Console.ReadLine());
                        choose = choose - 1;
                        {

                        }
                        found[choose].setValue(valueReplaced);
                        printer.Print(Root);
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Dữ liệu không hợp lệ. Vui lòng nhập lại số nguyên.");
                    }
                    catch (NullReferenceException)
                    {
                        Console.WriteLine("Dữ liệu nhập vào không được null.");

                    }

                }

            }

        }
    }

}

