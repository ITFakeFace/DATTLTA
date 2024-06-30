using System.CodeDom.Compiler;
using System.Xml.Linq;
using TreeManagementApplication.Model.BinaryTree;
using TreeManagementApplication.Model.Interface;

namespace TreeManagementApplication.Model.BinarySearchTree
{
    [Serializable]

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


        public String PrintLNR(INode<T>? Node, ref String result)
        {
            if (Node == null) { return ""; }

            PrintLNR(Node.GetLNode(), ref result);
            Console.Write(Node.GetValue()!.ToString() + "  ");
            result += Node.GetValue()!.ToString() + "  ";
            PrintLNR(Node.GetRNode(), ref result);
            return result;
        }

        public String PrintLRN(INode<T>? Node, ref String result)
        {
            if (Node == null) { return ""; }

            PrintLNR(Node.GetLNode(), ref result);
            PrintLNR(Node.GetRNode(), ref result);
            Console.Write(Node.GetValue()!.ToString() + "  ");
            result += Node.GetValue()!.ToString() + "  ";
            return result;
        }

        public String PrintNLR(INode<T>? Node, ref String result)
        {
            if (Node == null) { return ""; }

            Console.Write(Node.GetValue()!.ToString() + "  ");
            result += Node.GetValue()!.ToString() + "  ";
            PrintLNR(Node.GetLNode(), ref result);
            PrintLNR(Node.GetRNode(), ref result);
            return result;
        }

        public String PrintNRL(INode<T>? Node, ref String result)
        {
            if (Node == null) { return ""; }

            Console.Write(Node.GetValue()!.ToString() + "  ");
            result += Node.GetValue()!.ToString() + "  ";
            PrintLNR(Node.GetRNode(), ref result);
            PrintLNR(Node.GetLNode(), ref result);
            return result;
        }

        public String PrintRLN(INode<T>? Node, ref String result)
        {
            if (Node == null) { return ""; }

            PrintLNR(Node.GetRNode(), ref result);
            PrintLNR(Node.GetLNode(), ref result);
            Console.Write(Node.GetValue()!.ToString() + "  ");
            result += Node.GetValue()!.ToString() + "  ";
            return result;
        }

        public String PrintRNL(INode<T>? Node, ref String result)
        {
            if (Node == null) { return ""; }

            PrintLNR(Node.GetRNode(), ref result);
            Console.Write(Node.GetValue()!.ToString() + "  ");
            result += Node.GetValue()!.ToString() + "  ";
            PrintLNR(Node.GetLNode(), ref result);
            return result;
        }
        public INode<T>? FindParentNode(INode<T> node)
        {
            throw new NotImplementedException();
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
        public string Serialize()
        {
            List<String> serializeString = new List<String>();
            string convertToString = "3,";
            Serialize(Root!, serializeString);

            foreach (var item in serializeString)
            {
                convertToString += item.ToString() + ",";
            }
            return convertToString;

        }

        private void Serialize(BSNode<T>? bSNode, List<String> serializeString)
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

        public int Deserialize(Queue<Object> readFromFile)
        {
            int _status = 200;
            try
            {
                if (this.Root != null)
                {
                    this.Root = null;
                }
                Console.WriteLine(readFromFile.Count);
                object nodeVal = readFromFile.Dequeue();
                bool isEquals = nodeVal.ToString()!.Equals(@"#");
                if (isEquals)
                {
                    _status = 201;
                    return _status;
                }
                BSNode<T> Root = new BSNode<T>(ParseObjecttoT(nodeVal));
                SetRoot(Root);

                do
                {
                    nodeVal = readFromFile.Dequeue();
                    if (nodeVal.ToString()!.Equals("#"))
                    {
                        continue;
                    }
                    Root!.InsertNode(ParseObjecttoT(nodeVal));
                } while (readFromFile.Count > 0);
            }
            catch
            {
                _status = 201;
            }
            return _status;
        }

        private T ParseObjecttoT(object obj)
        {
            return (T)Convert.ChangeType(obj, typeof(T));
        }

        public string nodeTypetoString()
        {
            return "BSNode";
        }

        public string PrintLNR()
        {
            String result = "";
            return PrintLNR(Root, ref result);
        }

        public string PrintLRN()
        {
            String result = "";
            return PrintLRN(Root, ref result);
        }

        public string PrintNLR()
        {
            String result = "";
            return PrintNLR(Root, ref result);
        }

        public string PrintNRL()
        {
            String result = "";
            return PrintNRL(Root, ref result);
        }

        public string PrintRLN()
        {
            String result = "";
            return PrintRLN(Root, ref result);
        }

        public string PrintRNL()
        {
            String result = "";
            return PrintRNL(Root, ref result);
        }
    }
}
