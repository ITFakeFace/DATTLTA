using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using TreeManagementApplication.Model.Interface;

namespace TreeManagementApplication.Model.BinaryTree
{
    [Serializable]
    internal class AVLNode<T> : INode<T> where T : IComparable<T>
    {
        public T Key;
        public int Height;
        public AVLNode<T>? LNode, RNode;
        public int Level { get; set; } = 0;
        public int XIndex { get; set; } = 0;
        public AVLNode(T d)
        {
            Key = d;
            Height = 1;
        }
        public INode<T>? FindChildNode(INode<T> node, T value)
        {
            throw new NotImplementedException();
        }

        public int GetLevel()
        {
            return Level;
        }

        public INode<T>? GetLNode()
        {
            return this.LNode;
        }

        public INode<T>? GetRNode()
        {
            return this.RNode;
        }

        public T? GetValue()
        {
            return this.Key;
        }

        public int GetXIndex()
        {
            return this.XIndex;
        }

        public bool IsLeftest()
        {
            if (LNode != null)
                return false;
            return true;
        }

        public void SetLevel(int Level)
        {
            this.Level = Level;
        }

        public void SetLNode(INode<T> Node)
        {
            this.LNode = (AVLNode<T>?)Node;
        }

        public void SetRNode(INode<T> Node)
        {
            this.RNode = (AVLNode<T>)Node;
        }

        public void SetValue(T? Value)
        {
            this.Key = Value;
        }

        public void SetXIndex(int XIndex)
        {
            this.XIndex = XIndex;
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
    }
}
