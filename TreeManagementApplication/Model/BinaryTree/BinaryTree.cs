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
        public void UpdateNode(T value)
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

        public List<BNode<T>>? findNode(T value)
        {
            List<BNode<T>>? found = new List<BNode<T>>();
            findNodeRecursive(root, value, found);

            return found;
        }

        public void findNodeRecursive(BNode<T>? node, T value, List<BNode<T>> found)
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
                findNodeRecursive(node.lNode, value, found);
            }
            if (node.getRNode() != null)
            {
                findNodeRecursive(node.rNode, value, found);
            }
        }
        public void editNode(T nodeValue, T valueReplaced)
        {
            if (nodeValue == null) { return; }
            List<BNode<T>> found = new List<BNode<T>>();
            ConsoleBinaryTreePrinter<T> printer = new ConsoleBinaryTreePrinter<T>();
            found = findNode(nodeValue);
            if (found == null)
            {
                return;
            }
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
                    Console.WriteLine($"{i + 1}: Delete {i + 1} node");
                }
                Console.WriteLine($"Choose node to delete(input -1 to exit)");

                try
                {
                    int choose = -1;

                    choose = int.Parse(s: Console.ReadLine());
                    choose = choose - 1;
                    if (choose >= 0 && choose < found.Count)
                    {
                        found[choose].setValue(valueReplaced);
                    }

                    printer.Print(root);

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
