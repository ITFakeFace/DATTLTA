using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shapes;
using TreeManagementApplication.Model.Interface;
using TreeManagementApplication.Model.VisualModel;

namespace TreeManagementApplication.Model.BinaryTree
{
	internal class BNode<T> : INode<T> where T : IComparable<T>
	{
		public T? Value { get; set; }
		public NodeGUI<T> GUI { get; set; } = new NodeGUI<T>();
		public BNode<T>? LNode { get; set; } = null;
		public BNode<T>? RNode { get; set; } = null;
		public int Level { get; set; } = 0;
		public int XIndex { get; set; } = 0;
		public BNode(List<T> Values) : this(Values, 0)
		{

		}

		public BNode(List<T> Values, int Index)
		{
			Load(this, Values, Index, 0);
		}

		private void Load(BNode<T> Tree, List<T> Values, int Index, int Level)
		{
			if (Index >= Values.Count)
				return;

			Tree.Value = Values[Index];

			if (Index * 2 + 1 < Values.Count)
				Tree.LNode = new BNode<T>(Values, Index * 2 + 1);

			if (Index * 2 + 2 < Values.Count)
				Tree.RNode = new BNode<T>(Values, Index * 2 + 2);
		}

		public bool IsLeftest()
		{
			if (LNode == null)
			{
				return true;
			}
			return false;
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
			return this.Value;
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

		public NodeGUI<T> GetGUI()
		{
			return this.GUI;
		}

		public int GetLevel()
		{
			throw new NotImplementedException();
		}

		public int GetDepth()
		{
			throw new NotImplementedException();
		}

		public int GetXIndex()
		{
			throw new NotImplementedException();
		}
	}
}
