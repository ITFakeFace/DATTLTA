using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml;
using System.Xml.Linq;
using TreeManagementApplication.Model.BinaryTree;
using TreeManagementApplication.Model.Interface;

namespace TreeManagementApplication.Model.BinarySearchTree
{
    internal class AVLTree<T> : ITree<T> where T : IComparable<T>
    {
        AVLNode<T> Root;

        int height(AVLNode<T> N)
        {
            if (N == null)
                return 0;

            return N.Height;
        }

        // A utility function to get 
        // maximum of two integers  
        int max(int a, int b)
        {
            return (a > b) ? a : b;
        }

        AVLNode<T> rightRotate(AVLNode<T> y)
        {
            AVLNode<T> x = y.LNode;
            AVLNode<T> T2 = x.RNode;

            // Perform rotation  
            x.RNode = y;
            y.LNode = T2;

            // Update heights  
            y.Height = max(height(y.LNode),
                        height(y.RNode)) + 1;
            x.Height = max(height(x.LNode),
                        height(x.RNode)) + 1;

            // Return new Root  
            return x;
        }

        // A utility function to LNode 
        // rotate subtree rooted with x  
        // See the diagram given above.  
        AVLNode<T> leftRotate(AVLNode<T> x)
        {
            AVLNode<T> y = x.RNode;
            AVLNode<T> T2 = y.LNode;

            // Perform rotation  
            y.LNode = x;
            x.RNode = T2;

            // Update heights  
            x.Height = max(height(x.LNode),
                        height(x.RNode)) + 1;
            y.Height = max(height(y.LNode),
                        height(y.RNode)) + 1;

            // Return new Root  
            return y;
        }

        // Get Balance factor of node N  
        int getBalance(AVLNode<T> N)
        {
            if (N == null)
                return 0;

            return height(N.LNode) - height(N.RNode);
        }
        public INode<T> DeleteNode(T Value)
        {
            Root = (AVLNode<T>)Delete(Root, Value);
            return Root;
        }

        private AVLNode<T> minValueNode(AVLNode<T> node)
        {
            AVLNode<T> current = node;

            /* loop down to find the leftmost leaf */
            while (current.LNode != null)
                current = current.LNode;

            return current;
        }

        public INode<T> Delete(INode<T> Node, T key)
        {
            // STEP 1: PERFORM STANDARD BST DELETE 
            if (Node == null)
                return Node;

            AVLNode<T> convertedNode = Node as AVLNode<T>;

            // If the Key to be deleted is smaller than 
            // the convertedNode's Key, then it lies in LNode subtree 
            if (key.CompareTo(convertedNode!.Key) < 0)
                convertedNode.LNode = (AVLNode<T>)Delete(convertedNode.LNode, key);

            // If the Key to be deleted is greater than the 
            // convertedNode's Key, then it lies in RNode subtree 
            else if (key.CompareTo(convertedNode.Key) > 0)
                convertedNode.RNode = (AVLNode<T>)Delete(convertedNode.RNode, key);

            // if Key is same as convertedNode's Key, then this is the node 
            // to be deleted 
            else
            {

                // node with only one child or no child 
                if ((convertedNode.LNode == null) || (convertedNode.RNode == null))
                {
                    AVLNode<T> temp = null;
                    if (temp == convertedNode.LNode)
                        temp = convertedNode.RNode;
                    else
                        temp = convertedNode.LNode;

                    // No child case 
                    if (temp == null)
                    {
                        temp = convertedNode;
                        convertedNode = null;
                    }
                    else // One child case 
                        convertedNode = temp; // Copy the contents of 
                                              // the non-empty child 
                }
                else
                {

                    // node with two children: Get the inorder 
                    // successor (smallest in the RNode subtree) 
                    AVLNode<T> temp = minValueNode(convertedNode.RNode);

                    // Copy the inorder successor's data to this node 
                    convertedNode.Key = temp.Key;

                    // Delete the inorder successor 
                    convertedNode.RNode = (AVLNode<T>)Delete(convertedNode.RNode, temp.Key);
                }
            }

            // If the tree had only one node then return 
            if (convertedNode == null)
                return convertedNode;

            // STEP 2: UPDATE HEIGHT OF THE CURRENT NODE 
            convertedNode.Height = max(height(convertedNode.LNode),
                        height(convertedNode.RNode)) + 1;

            // STEP 3: GET THE BALANCE FACTOR
            // OF THIS NODE (to check whether 
            // this node became unbalanced) 
            int balance = getBalance(convertedNode);

            // If this node becomes unbalanced, 
            // then there are 4 cases 
            // LNode LNode Case 
            if (balance > 1 && getBalance(convertedNode.LNode) >= 0)
                return rightRotate(convertedNode);

            // LNode RNode Case 
            if (balance > 1 && getBalance(convertedNode.LNode) < 0)
            {
                convertedNode.LNode = leftRotate(convertedNode.LNode);
                return rightRotate(convertedNode);
            }

            // RNode RNode Case 
            if (balance < -1 && getBalance(convertedNode.RNode) <= 0)
                return leftRotate(convertedNode);

            // RNode LNode Case 
            if (balance < -1 && getBalance(convertedNode.RNode) > 0)
            {
                convertedNode.RNode = rightRotate(convertedNode.RNode);
                return leftRotate(convertedNode);
            }

            return convertedNode;
        }

        public INode<T>? FindNode(T Value)
        {
            throw new NotImplementedException();
        }

        public INode<T>? FindNode(int XIndex, int Level)
        {
            throw new NotImplementedException();
        }

        public INode<T>? FindParentNode(INode<T> node, int XIndex)
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

        public INode<T>? GetRoot()
        {
            return this.Root;
        }

        public List<T>? GetValues()
        {
            throw new NotImplementedException();
        }

        public bool InsertNode(T Value)
        {
            Root = (AVLNode<T>)Insert(Root, Value);
            return true;
        }

        public INode<T> Insert(INode<T> node, T key)
        {
            if (node == null)
                return (new AVLNode<T>(key));

            AVLNode<T> convertedNode = node as AVLNode<T>;

            if (key.CompareTo(convertedNode!.Key) < 0)
                convertedNode.LNode = (AVLNode<T>)Insert(convertedNode.LNode, key);
            else if (key.CompareTo(convertedNode.Key) > 0)
                convertedNode.RNode = (AVLNode<T>)Insert(convertedNode.RNode, key);
            else
                return convertedNode;

            convertedNode.Height = 1 + max(height(convertedNode.LNode), height(convertedNode.RNode));

            int balance = getBalance(convertedNode);

            if (balance > 1 && key.CompareTo(convertedNode.LNode.Key) < 0)
                return rightRotate(convertedNode);

            if (balance < -1 && key.CompareTo(convertedNode.RNode.Key) > 0)
                return leftRotate(convertedNode);

            if (balance > 1 && key.CompareTo(convertedNode.LNode.Key) > 0)
            {
                convertedNode.LNode = leftRotate(convertedNode.LNode);
                return rightRotate(convertedNode);
            }

            if (balance < -1 && key.CompareTo(convertedNode.RNode.Key) < 0)
            {
                convertedNode.RNode = rightRotate(convertedNode.RNode);
                return leftRotate(convertedNode);
            }

            return convertedNode;
        }
        public bool IsEmpty()
        {
            return Root == null;
        }

        public void PrintConsole()
        {
            ConsoleBinaryTreePrinter<T> printer = new ConsoleBinaryTreePrinter<T>();
            printer.Print(Root);
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

        public void SetRoot(INode<T> Node)
        {
            this.Root = (AVLNode<T>)Node;
        }

        public bool UpdateNode(INode<T> Node, T value)
        {
            throw new NotImplementedException();
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
        public int GetLargestY()
        {
            return GetLargestY(Root);
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

        public List<string> Serialize()
        {
            throw new NotImplementedException();
        }

        public void Deserialize(Queue<object> readFromFile)
        {
            throw new NotImplementedException();
        }
    }
}
