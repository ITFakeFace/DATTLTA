using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shapes;

namespace TreeManagementApplication.Model.BinaryTree
{
	internal class BNode<T> where T : IComparable<T>
	{
		public T? value { get; set; }
		public BNode<T>? lNode { get; set; } = null;
		public BNode<T>? rNode { get; set; } = null;
		public Rectangle nodeObject { get; set; } = new Rectangle();

		public BNode(List<T> values) : this(values, 0)
		{

		}

		public BNode(List<T> values, int index)
		{
			Load(this, values, index);
		}

		private void Load(BNode<T> tree, List<T> values, int index)
		{
			if (index >= values.Count)
				return;

			tree.value = values[index];

			if (index * 2 + 1 < values.Count)
				tree.lNode = new BNode<T>(values, index * 2 + 1);

			if (index * 2 + 2 < values.Count)
				tree.rNode = new BNode<T>(values, index * 2 + 2);
		}

		public void insertData(int data)
		{

		}


	}
}
