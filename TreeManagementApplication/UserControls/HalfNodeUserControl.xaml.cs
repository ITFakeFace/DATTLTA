using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TreeManagementApplication.UserControls
{
	/// <summary>
	/// Interaction logic for HalfNodeUserControl.xaml
	/// </summary>
	public partial class HalfNodeUserControl : UserControl
	{
		public enum NodeType { Left, Right }
		public NodeType type;
		public HalfNodeUserControl()
		{
			InitializeComponent();
		}

		public void InitializeProperties()
		{
			if (type == NodeType.Left)
			{

			}
		}

		public void OnHover()
		{

		}

		public void OnUnHover()
		{

		}
	}
}
