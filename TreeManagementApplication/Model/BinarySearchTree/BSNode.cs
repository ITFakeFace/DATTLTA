using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;
using TreeManagementApplication.Model.BinaryTree;
using TreeManagementApplication.Model.Interface;
using TreeManagementApplication.Model.VisualModel;

namespace TreeManagementApplication.Model.BinarySearchTree
{
	internal class BSNode<T> : INode<T> where T : IComparable<T>
	{
		public T? Value { get; set; }
		public NodeGUI<T> GUI { get; set; } = new NodeGUI<T>();
		public int Level { get; set; } = 0;
		public int XIndex { get; set; } = 0;
		public BSNode<T>? LNode { get; set; } = null;
		public BSNode<T>? RNode { get; set; } = null;
		public BSNode(T Value)
		{
			this.Value = Value;
		}

		public BSNode(T Value, BSNode<T> LNode, BSNode<T> RNode)
		{
			this.Value = Value;
			this.LNode = LNode;
			this.RNode = RNode;
		}

		public BSNode<T> initialize(T Value)
		{
			return new BSNode<T>(Value);
		}
		public bool IsLeftest()
		{
			if (LNode == null)
			{
				return true;
			}
			return false;
		}
		public bool InsertNode(T Value)
		{
			bool result = false;
			if (this.Value!.CompareTo(Value) > 0)
			{
				if (LNode == null)
				{
					LNode = new BSNode<T>(Value);
					result = true;
				}
				else
				{
					LNode.InsertNode(Value);
					result = true;
				}
			}
			else
			{
				if (RNode == null)
				{
					RNode = new BSNode<T>(Value);
					result = true;
				}
				else
				{
					RNode.InsertNode(Value);
					result = true;
				}
			}
			return result;
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

		public override String ToString()
		{
			return Value + "";
		}

		public INode<T>? FindChildNode(INode<T> Node, T Value)
		{
			throw new NotImplementedException();
		}

		public NodeGUI<T> GetGUI()
		{
			return this.GUI;
		}

		public int GetLevel()
		{
			return this.Level;
		}

		public int GetXIndex()
		{
			return this.XIndex;
		}
	}
}