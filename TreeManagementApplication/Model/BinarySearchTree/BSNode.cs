using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;
using TreeManagementApplication.Model.BinaryTree;
using TreeManagementApplication.Model.Interface;

namespace TreeManagementApplication.Model.BinarySearchTree
{
	internal class BSNode<T> : INode<T> where T : IComparable<T>
	{
		public T? value { get; set; }
		public BSNode<T>? lNode { get; set; } = null;
		public BSNode<T>? rNode { get; set; } = null;
		public BSNode(T value)
		{
			this.value = value;
		}

		public BSNode(T value, BSNode<T> lNode, BSNode<T> rNode)
		{
			this.value = value;
			this.lNode = lNode;
			this.rNode = rNode;
		}

		public BSNode<T> initialize(T value)
		{
			return new BSNode<T>(value);
		}

		public bool InsertNode(T value)
		{
			bool result = false;
			if (this.value!.CompareTo(value) > 0)
			{
				if (lNode == null)
				{
					lNode = new BSNode<T>(value);
					result = true;
				}
				else
				{
					lNode.InsertNode(value);
					result = true;
				}
			}
			else
			{
				if (rNode == null)
				{
					rNode = new BSNode<T>(value);
					result = true;
				}
				else
				{
					rNode.InsertNode(value);
					result = true;
				}
			}
			return result;
		}

		public INode<T>? getLNode()
		{
			return this.lNode;
		}

		public INode<T>? getRNode()
		{
			return this.rNode;
		}

		public T? getValue()
		{
			return this.value;
		}

		public void setValue(T? value)
		{
			this.value = value;
		}

		public override String ToString()
		{
			return value + "";
		}
	}
}