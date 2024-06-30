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


        public String PrintLNR(INode<T>? Node, String result = "")
        {
            if (Node == null) { return ""; }

            PrintLNR(Node.GetLNode());
            Console.Write(Node.GetValue()!.ToString() + "  ");
            result += Node.GetValue()!.ToString() + "  ";
            PrintLNR(Node.GetRNode());
            return result;
        }

        public String PrintLRN(INode<T>? Node, String result = "")
        {
            if (Node == null) { return ""; }

            PrintLNR(Node.GetLNode());
            PrintLNR(Node.GetRNode());
            Console.Write(Node.GetValue()!.ToString() + "  ");
            result += Node.GetValue()!.ToString() + "  ";
            return result;
        }

        public String PrintNLR(INode<T>? Node, String result = "")
        {
            if (Node == null) { return ""; }

            Console.Write(Node.GetValue()!.ToString() + "  ");
            result += Node.GetValue()!.ToString() + "  ";
            PrintLNR(Node.GetLNode());
            PrintLNR(Node.GetRNode());
            return result;
        }

        public String PrintNRL(INode<T>? Node, String result = "")
        {
            if (Node == null) { return ""; }

            Console.Write(Node.GetValue()!.ToString() + "  ");
            result += Node.GetValue()!.ToString() + "  ";
            PrintLNR(Node.GetRNode());
            PrintLNR(Node.GetLNode());
            return result;
        }

        public String PrintRLN(INode<T>? Node, String result = "")
        {
            if (Node == null) { return ""; }

            PrintLNR(Node.GetRNode());
            PrintLNR(Node.GetLNode());
            Console.Write(Node.GetValue()!.ToString() + "  ");
            result += Node.GetValue()!.ToString() + "  ";
            return result;
        }

        public String PrintRNL(INode<T>? Node, String result = "")
        {
            if (Node == null) { return ""; }

            PrintLNR(Node.GetRNode());
            Console.Write(Node.GetValue()!.ToString() + "  ");
            result += Node.GetValue()!.ToString() + "  ";
            PrintLNR(Node.GetLNode());
            return result;
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
                return parentNode.InsertNode(value);
            }
            else
            {
                INode<T> newNode = new BSNode<T>(value, (BSNode<T>)Root!.GetLNode()!, (BSNode<T>)Root!.GetRNode()!);
                DeleteNode(Root!, node.GetValue()!);
                SetRoot(newNode);
                return true;
            }
        }

        public INode<T>? FindAndDeleteNode(T? value, INode<T>? node = null)
        {
            if (node is null)
            {// Tìm node có giá trị value 
                INode<T>? nodeToDelete = FindNode(value);

                if (nodeToDelete != null)
                {
                    // Xóa node và trả về node đã xóa 
                    Root = (BSNode<T>?)DeleteNode(Root, value);
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
        private INode<T>? DeleteNode(INode<T>? root, T value)
        {
            if (root == null)
                return null;

            int compare = value.CompareTo(root.GetValue());

            if (compare < 0)
            {
                root.SetLNode(DeleteNode(root.GetLNode(), value));
            }
            else if (compare > 0)
            {
                root.SetRNode(DeleteNode(root.GetRNode(), value));
            }
            else
            {
                // Node found with value equals 'value' 

                // Case 1: No child or only one child 
                if (root.GetLNode() == null)
                {
                    return root.GetRNode();
                }
                else if (root.GetRNode() == null)
                {
                    return root.GetLNode();
                }

                // Case 2: Node with two children 
                // Get the inorder successor (smallest in the right subtree) 
                root.SetValue(MinValue(root.GetRNode()));

                // Delete the inorder successor 
                root.SetRNode(DeleteNode(root.GetRNode(), root.GetValue()));
            }

            return root;
        }

        private T MinValue(INode<T> node)
        {
            T minValue = node.GetValue();
            while (node.GetLNode() != null)
            {
                minValue = node.GetLNode().GetValue();
                node = node.GetLNode();
            }
            return minValue;
        }

        public INode<T>? FindNode(T Value)
        {
            if (this.GetRoot() == null) { return null; }
            return FindChildNode(this.Root, Value);
        }

        public INode<T>? FindChildNode(INode<T>? node, T value)
        {
            if (node == null) { return node; }
            int isEqual = node.GetValue()!.CompareTo(value);

            if (isEqual == 0)
            {
                return node;
            }
            else if (isEqual < 0)
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


    }
}
