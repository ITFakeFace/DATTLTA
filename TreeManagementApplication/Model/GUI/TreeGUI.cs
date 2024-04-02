using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TreeManagementApplication.Model.Interface;

namespace TreeManagementApplication.Model.GUI
{
	class TreeGUI<T> where T : IComparable<T>
	{
		INode<T> root;
		public void Print()
		{

		}
	}
}
