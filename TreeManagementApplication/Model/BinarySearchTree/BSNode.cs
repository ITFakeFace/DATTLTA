using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Xml.Linq;
using TreeManagementApplication.Model.BinaryTree;
using TreeManagementApplication.Model.Interface;
using TreeManagementApplication.Model.VisualModel;
using static System.Net.Mime.MediaTypeNames;

namespace TreeManagementApplication.Model.BinarySearchTree
{
	internal class BSNode<T> : INode<T> where T : IComparable<T>
	{
		public T? Value { get; set; }
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
			if (this.GetLNode() == null)
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
			else if (this.Value!.CompareTo(Value) < 0)
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
		public override String ToString()
		{
			return " (" + this.XIndex + "," + this.Level + ") ";
			//return " (" + this.Value + "," + this.XIndex + ") ";
			//return $" ({this.Value},{CountChildNode(this)}) ";
		}
		public INode<T>? FindChildNode(INode<T> Node, T Value)
		{
			throw new NotImplementedException();
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
		//Get-Set LNode
		public INode<T>? GetLNode()
		{
			return this.LNode;
		}
		void SetLNode(INode<T> Node)
		{
			this.LNode = (BSNode<T>?)Node;
		}

		void INode<T>.SetLNode(INode<T> Node)
		{
			this.LNode = (BSNode<T>)Node;
		}
		//Get-Set RNode
		public INode<T>? GetRNode()
		{
			return this.RNode;
		}
		void SetRNode(INode<T> Node)
		{
			this.RNode = (BSNode<T>?)Node;
		}
		void INode<T>.SetRNode(INode<T> Node)
		{
			this.RNode = (BSNode<T>?)Node;
		}
		//Get-Set Value
		public T? GetValue()
		{
			return this.Value;
		}
		public void SetValue(T? Value)
		{
			this.Value = Value;
		}
		//Get-Set XIndex
		public int GetXIndex()
		{
			return this.XIndex;
		}
		public void SetXIndex(int XIndex)
		{
			this.XIndex = XIndex;
		}
		void INode<T>.SetXIndex(int XIndex)
		{
			this.XIndex = XIndex;
		}
		//Get-Set Level
		public int GetLevel()
		{
			return this.Level;
		}
		public void SetLevel(int Level)
		{
			this.Level = Level;
		}
		void INode<T>.SetLevel(int Level)
		{
			this.Level = Level;
		}

	}
}