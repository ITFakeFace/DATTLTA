using System.CodeDom.Compiler;
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
            return this.Root;
        }

        public void DeleteTree(BSNode<T> root)
        {
            if (root == null)
            {
                Values = null;
                return;
            }

            // Duyệt qua từng nút con và giải phóng bộ nhớ
            if (root.LNode == null)
            {
                DeleteTree(root.LNode!);
            }
            if (root.RNode == null)
            {
                DeleteTree(root.RNode!);
            }

            // Giải phóng bộ nhớ của nút hiện tại
            root = null;
        }
        public void SetRoot(INode<T> Node)
        {
            if (Node is BSNode<T>)
            {
                this.Root = (BSNode<T>)Node;
            }
            else
            {
                Console.WriteLine("Node is not Root of Binary Tree");
            }
        }

        public void PrintConsole()
        {
            ConsoleBinaryTreePrinter<T> printer = new ConsoleBinaryTreePrinter<T>();
            printer.Print(Root);
        }

        public bool InsertNode(T Value)
        {
            if (this.Root == null)
            {
                this.Root = new BSNode<T>(Value);
                Values!.Add(Value);
                return true;
            }
            else
            {
                bool result = this.Root.InsertNode(Value);
                if (result)
                {
                    Values!.Add(Value);
                }
                return result;
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
        public bool UpdateNode(INode<T> node, T value)
        {
            if (!node.Equals(Root))
            {
                var result = FindParentNode(Root!, node.GetXIndex());
                BSNode<T> parentNode = (BSNode<T>)result!;
                DeleteNode(parentNode, node.GetValue()!);
                parentNode.InsertNode(value);
                return true;
            }
            else
            {
                INode<T> newNode = new BSNode<T>(value, (BSNode<T>)Root!.GetLNode()!, (BSNode<T>)Root!.GetRNode()!);
                DeleteNode(Root!, node.GetValue()!);
                SetRoot(newNode);
                return true;
            }
        }

        public void DeleteNode(int XIndex, int level)
        {
            INode<T> node = FindNode(XIndex, level);
            Console.WriteLine(node);
            Root = (BSNode<T>?)DeleteNode(Root, node.GetValue());

        }
        private BSNode<T>? DeleteNode(INode<T>? node, T value)
        {
            if (node == null)
            {
                return null;
            }
            BSNode<T> convert = node as BSNode<T>;
            if (value.CompareTo(convert.GetValue()) < 0)
            {
                convert.LNode = (BSNode<T>)DeleteNode(convert.GetLNode(), value);

            }
            else if (value.CompareTo(convert.GetValue()) > 0)
            {
                convert.RNode = (BSNode<T>)DeleteNode(convert.GetRNode(), value);
            }
            else
            {
                if ((convert.GetLNode() == null) || (convert.GetRNode() == null))
                {
                    BSNode<T> res = null;
                    if (res == convert.GetLNode())
                        res = convert.RNode;
                    else
                        res = convert.LNode;
                    if (res == null)
                    {
                        res = convert;
                        convert = null;
                    }
                    else
                        convert = res;
                }
                else
                {
                    BSNode<T> res = MinValue(convert.RNode);

                    convert.Value = res.Value;
                    convert.RNode = (BSNode<T>)DeleteNode(convert.RNode, res.Value);

                }

            }
            return convert;
        }

        private BSNode<T> MinValue(BSNode<T>? node)
        {
            BSNode<T> current = node;
            while (current.GetLNode() != null)
            {
                current = current.LNode;
            }
            return current;
        }

        public INode<T>? FindNode(int XIndex, int Level)
        {
            return Root!.FindNode(XIndex, Level);
        }

        public INode<T>? FindNode(T Value)
        {
            return FindNode(Root, Value);
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
        public int GetLargestX(INode<T> Node)
        {
            if (Node.GetRNode() == null)
            {
                return Node.GetXIndex();
            }
            return GetLargestX(Node.GetRNode()!);
        }
        public int GetLargestY()
        {
            return GetLargestY(Root);
        }

        public void GenerateGridIndex()
        {
            if (Root == null) { return; }
            int pos = 0;
            Root.CalcX(ref pos);
            pos = 0;
            Root.CalcY(pos);
        }

        public INode<T> DeleteNode(T Value)
        {
            throw new NotImplementedException();
        }

        public List<T>? GetValues()
        {
            return this.Values;
        }

        public ITree<T> GenerateRandomTree(int Count, int Min, int Max)
        {
            BinarySearchTree<int> Tree = new BinarySearchTree<int>();
            HashSet<int> valueList = new HashSet<int>();
            Random rand = new Random();
            while (valueList.Count < Count)
            {
                valueList.Add(rand.Next(Min, Max));
            }
            foreach (var ele in valueList)
            {
                Tree.InsertNode(ele);
            }
            return (ITree<T>)Tree;
        }

        public INode<T>? FindParentNode(INode<T> node)
        {
            throw new NotImplementedException();
        }

        public List<String> Serialize()
        {
            List<String> serializeString = new List<String>();
            Serialize(Root!, serializeString);
            foreach (var item in serializeString)
            {
                Console.WriteLine(item.ToString());
            }


            return serializeString;

        }

        private void Serialize(BSNode<T> bSNode, List<String> serializeString)
        {
            if (bSNode == null)
            {
                serializeString.Add("#");
                return;
            }
            serializeString.Add(bSNode.GetValue().ToString());
            Serialize(bSNode.LNode, serializeString);
            Serialize(bSNode.RNode, serializeString);
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

        public void DeleteBnode(INode<T> node)
        {
            throw new NotImplementedException();
        }



    }
}
