using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TreeManagementApplication.Model.VisualModel;

namespace TreeManagementApplication.Model.Interface
{
	public interface INode<T> where T : IComparable<T>
	{
		//Get-Set LNode
		void SetLNode(INode<T> Node);
		INode<T>? GetLNode();
		void setValue(T? value);

		//Get-Set RNode
		INode<T>? GetRNode();
		void SetRNode(INode<T> Node);

		//Get-Set Value
		T? GetValue();
		void SetValue(T? Value);

		//Get-Set XIndex
		int GetXIndex();
		void SetXIndex(int XIndex);

		//Get-Set Level
		int GetLevel();
		void SetLevel(int Level);

		//Sub Check Methods
		bool IsLeftest();
		String? ToString();
		INode<T>? FindChildNode(INode<T> node, T value);
	}
}
