using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TreeManagementApplication.Model.BinaryTree;

namespace TreeManagementApplication.Model.Interface
{
	interface ITree<T> where T : IComparable<T>
	{
		void Print();
		void InsertNode(T value);
		void UpdateNode(T value);
		void RemoveNode(T value);


		void PrintLNR(INode<T>? node);
		void PrintLRN(INode<T>? node);
		void PrintNLR(INode<T>? node);
		void PrintNRL(INode<T>? node);
		void PrintRLN(INode<T>? node);
		void PrintRNL(INode<T>? node);
		public List<INode<T>>? findNode(T value);
        public void findNodeRecursive(INode<T>? node, T value, List<INode<T>> found);



    }
}
