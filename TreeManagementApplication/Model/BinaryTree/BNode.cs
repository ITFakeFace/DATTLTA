using TreeManagementApplication.Model.Interface;

namespace TreeManagementApplication.Model.BinaryTree
{
    [Serializable]
    internal class BNode<T> : INode<T> where T : IComparable<T>
    {
        public T? Value { get; set; }
        public BNode<T>? LNode { get; set; } = null;
        public BNode<T>? RNode { get; set; } = null;
        public int Level { get; set; } = 0;
        public int XIndex { get; set; } = 0;
        /*
		public BNode(List<T> Values) : this(Values, 0)
		{

		}

		public BNode(List<T> Values, int Index)
		{
			Import(this, Values, Index, 0);
		}

		private void Import(BNode<T> Tree, List<T> Values, int Index, int Level)
		{
			if (Index >= Values.Count)
				return;

			Tree.Value = Values[Index];

			if (Index * 2 + 1 < Values.Count)
				Tree.LNode = new BNode<T>(Values, Index * 2 + 1);

			if (Index * 2 + 2 < Values.Count)
				Tree.RNode = new BNode<T>(Values, Index * 2 + 2);
		}
		*/
        public BNode(T value)
        {
            this.Value = value;
            this.LNode = null;
            this.RNode = null;
        }

        public void InsertNode(INode<T> Node, T Value)
        {
            if (Node == null)
            {
                Node = new BNode<T>(Value);
                return;
            }
            Queue<INode<T>> queue = new Queue<INode<T>>();
            queue.Enqueue(this);

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
                {
                    queue.Enqueue(Node.GetRNode()!);
                }
            }
        }

        /*   public void InsertLeft(T Value)
           {
               this.LNode = new BNode<T>(Value);
           }

           public void InsertRight()
           {

           }*/
        public bool IsLeftest()
        {
            if (LNode == null)
            {
                return true;
            }
            return false;
        }
        public override String ToString()
        {
            return $"({this.Value})";
            //return " (" + this.XIndex + "," + this.Level + ") ";
            //return " (" + this.Value + "," + this.XIndex + ") ";
            //return $" ({this.Value},{CountChildNode(this)}) ";
        }

        public void CalcX(ref int CurrentX)
        {
            if (this == null)
            {
                return;
            }
            this.LNode?.CalcX(ref CurrentX);

            this.SetXIndex(CurrentX);
            CurrentX++;
            this.RNode?.CalcX(ref CurrentX);
        }
        public void CalcY(int CurrentY)
        {
            if (this == null)
            {
                return;
            }
            this.SetLevel(CurrentY++);
            this.LNode?.CalcY(CurrentY);
            this.RNode?.CalcY(CurrentY);
        }

        public int CountChildNode(INode<T>? Node)
        {
            if (Node == null)
            {
                return 0;
            }
            return CountChildNode(Node.GetLNode()) + CountChildNode(Node.GetRNode()) + 1;
        }
        public INode<T>? GetLNode()
        {
            return this.LNode;
        }

        public INode<T>? GetRNode()
        {
            return this.RNode;
        }
        /*   void SetLNode(INode<T> Node)
           {
               this.LNode = (BNode<T>)Node;
           }
           void SetRNode(INode<T> Node)
           {
               this.RNode = (BNode<T>)Node;
           }*/
        public T? GetValue()
        {
            return this.Value;
        }

        void SetXIndex(int XIndex)
        {
            this.XIndex = XIndex;
        }
        void SetLevel(int Level)
        {
            this.Level = Level;
        }

        public INode<T>? FindChildNode(INode<T>? Node, T Value)
        {
            INode<T>? result = null;
            if (Node == null)
                return null;

            if (Node.GetValue()!.CompareTo(Value) == 0)
                result = Node;
            if (result != null)
                result = FindChildNode(Node.GetLNode(), Value);
            if (result != null)
                result = FindChildNode(Node.GetRNode(), Value);
            return result;
        }
        public INode<T>? FindNode(int XIndex, int Level)
        {
            if (this == null) { return null; }
            if (this.XIndex == XIndex)
            {
                return this;
            }
            if (XIndex < this.XIndex)
            {
                return this.LNode!.FindNode(XIndex, Level);
            }
            else if (XIndex > this.XIndex)
            {
                return this.RNode!.FindNode(XIndex, Level);
            }
            return null;
        }
        public int GetLevel()
        {
            return this.Level;
        }

        /* public int GetDepth()
         {
             return this.XIndex;
         }*/

        public int GetXIndex()
        {
            return this.XIndex;
        }

        void INode<T>.SetLNode(INode<T> Node)
        {
            this.LNode = (BNode<T>)Node;
        }

        void INode<T>.SetRNode(INode<T> Node)
        {
            this.RNode = (BNode<T>)Node;
        }

        public void SetValue(T? Value)
        {
            this.Value = Value;
        }

        void INode<T>.SetXIndex(int XIndex)
        {
            this.XIndex = XIndex;
        }

        void INode<T>.SetLevel(int Level)
        {
            this.Level = Level;
        }
    }
}
