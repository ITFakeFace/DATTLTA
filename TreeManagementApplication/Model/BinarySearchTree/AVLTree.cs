using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TreeManagementApplication.Model.Interface;

namespace TreeManagementApplication.Model.AVLTree
{
    internal class AVLTree<T> : ITree<T> where T : IComparable<T>
    {
        public INode<T> DeleteNode(T Value)
        {
            throw new NotImplementedException();
        }

        public INode<T>? FindNode(T Value)
        {
            throw new NotImplementedException();
        }

        public INode<T>? FindNode(int XIndex, int Level)
        {
            throw new NotImplementedException();
        }

        public INode<T>? FindParentNode(INode<T> node)
        {
            throw new NotImplementedException();
        }

        public void GenerateGridIndex()
        {
            throw new NotImplementedException();
        }

        public ITree<T> GenerateRandomTree(int Count, int Min, int Max)
        {
            throw new NotImplementedException();
        }

        public int GetLargestX(INode<T> Node)
        {
            throw new NotImplementedException();
        }

        public int GetLargestY(INode<T> Node)
        {
            throw new NotImplementedException();
        }

        public INode<T>? GetRoot()
        {
            throw new NotImplementedException();
        }

        public List<T>? GetValues()
        {
            throw new NotImplementedException();
        }

        public bool InsertNode(T Value)
        {
            throw new NotImplementedException();
        }

        public bool IsEmpty()
        {
            throw new NotImplementedException();
        }

        public void PrintConsole()
        {
            throw new NotImplementedException();
        }

        public void PrintLNR(INode<T>? Node)
        {
            throw new NotImplementedException();
        }

        public void PrintLRN(INode<T>? Node)
        {
            throw new NotImplementedException();
        }

        public void PrintNLR(INode<T>? Node)
        {
            throw new NotImplementedException();
        }

        public void PrintNRL(INode<T>? Node)
        {
            throw new NotImplementedException();
        }

        public void PrintRLN(INode<T>? Node)
        {
            throw new NotImplementedException();
        }

        public void PrintRNL(INode<T>? Node)
        {
            throw new NotImplementedException();
        }

        public List<string> Serialize()
        {
            throw new NotImplementedException();
        }

        public void SetRoot(INode<T> Node)
        {
            throw new NotImplementedException();
        }

        public bool UpdateNode(INode<T> Node, T value)
        {
            throw new NotImplementedException();
        }
    }
}
