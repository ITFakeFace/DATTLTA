using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TreeManagementApplication.Model.Interface;

namespace TreeManagementApplication.Model.BinaryTree
{
	internal class AVLNode<T> : INode<T> where T : IComparable<T>
	{
		public INode<T>? FindChildNode(INode<T> node, T value)
		{
			throw new NotImplementedException();
		}

		public int GetLevel()
		{
			throw new NotImplementedException();
		}

		public INode<T>? GetLNode()
		{
			throw new NotImplementedException();
		}

		public INode<T>? GetRNode()
		{
			throw new NotImplementedException();
		}

		public T? GetValue()
		{
			throw new NotImplementedException();
		}

		public int GetXIndex()
		{
			throw new NotImplementedException();
		}

		public bool IsLeftest()
		{
			throw new NotImplementedException();
		}

		public void SetLevel(int Level)
		{
			throw new NotImplementedException();
		}

		public void SetLNode(INode<T> Node)
		{
			throw new NotImplementedException();
		}

		public void SetRNode(INode<T> Node)
		{
			throw new NotImplementedException();
		}

		public void setValue(T? value)
		{
			throw new NotImplementedException();
		}

		public void SetValue(T? Value)
		{
			throw new NotImplementedException();
		}

		public void SetXIndex(int XIndex)
		{
			throw new NotImplementedException();
		}
	}
}
