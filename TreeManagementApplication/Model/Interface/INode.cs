using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeManagementApplication.Model.Interface
{
	interface INode<T> where T : IComparable<T>
	{
		INode<T>? getLNode();
		INode<T>? getRNode();

		T? getValue();

		String? ToString();
	}
}
