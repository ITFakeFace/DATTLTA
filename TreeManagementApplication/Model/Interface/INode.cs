using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TreeManagementApplication.Model.VisualModel;

namespace TreeManagementApplication.Model.Interface
{
	interface INode<T> where T : IComparable<T>
	{
		INode<T>? GetLNode();
		INode<T>? GetRNode();
		T? GetValue();
		int GetLevel();
		int GetXIndex();
		bool IsLeftest();
		String? ToString();
		INode<T>? FindChildNode(INode<T> node, T value);
		NodeGUI<T> GetGUI();
	}
}
