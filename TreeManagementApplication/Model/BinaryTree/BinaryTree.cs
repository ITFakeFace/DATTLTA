using System.Diagnostics.Eventing.Reader;
using TreeManagementApplication.Model.BinarySearchTree;
using TreeManagementApplication.Model.Interface;

namespace TreeManagementApplication.Model.BinaryTree
{
    [Serializable]
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
                return 0;
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

        INode<T>? ITree<T>.FindNode(T Value)
        {
            throw new NotImplementedException();
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

        public string Serialize()
        {
            List<String> serializeString = new List<String>();
            Serialize(Root!, serializeString);
            string convertToString = "2,";

            foreach (var item in serializeString)
            {
                convertToString += item.ToString() + ",";
            }
            return convertToString;
        }


        private void Serialize(BNode<T>? bNode, List<String> serializeString)
        {
            if (bNode == null)
            {
                serializeString.Add("#");
                return;
            }
            serializeString.Add(bNode.GetValue().ToString());
            Serialize(bNode.LNode, serializeString);
            Serialize(bNode.RNode, serializeString);
        }
        public int GetLargestY(INode<T> Node)
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

        public int Deserialize(Queue<Object> readFromFile)
        {
            int _status = 200;
            try
            {
                if (this.Root != null)
                {
                    this.Root = null;
                }
                BNode<T> Root = new BNode<T>(ParseObjecttoT(readFromFile.Dequeue()));
                SetRoot(Root);
                Deserialize(Root, readFromFile);
            }
            catch
            {
                _status = 201;

            }
            return _status;
        }

        private void Deserialize(INode<T> node, Queue<object> valueQueue)
        {
            if (valueQueue.Count == 0)
            {
                return;
            }
            object leftNodeVal = valueQueue.Dequeue();
            if (!leftNodeVal.ToString()!.Equals("#"))
            {
                INode<T> leftNode = new BNode<T>(ParseObjecttoT(leftNodeVal));
                node.SetLNode(leftNode);
                Deserialize(leftNode, valueQueue);
            }
            if (valueQueue.Count == 0)
            {
                return;
            }
            object rightNodeVal = valueQueue.Dequeue();
            if (!rightNodeVal.ToString()!.Equals("#"))
            {
                INode<T> rightNode = new BNode<T>(ParseObjecttoT(rightNodeVal));
                node.SetRNode(rightNode);
                Deserialize(rightNode, valueQueue);
            }
        }

        private T ParseObjecttoT(object obj)
        {
            return (T)Convert.ChangeType(obj, typeof(T));
        }

        public string nodeTypetoString()
        {
            return "BNode";
        }
    }
}
