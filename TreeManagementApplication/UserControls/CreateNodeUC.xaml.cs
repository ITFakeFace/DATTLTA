using System;
using System.Collections.Generic;
using System.DirectoryServices;
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
	public partial class CreateNodeUC : UserControl
	{
		public bool HasLeft { get; set; }

		public bool HasRight { get; set; }


		public CreateNodeUC()
		{
			InitializeComponent();
			InititalizeProperties();
		}

		public void InititalizeProperties()
		{
			Canvas.SetZIndex(HalfLeft, 5);
			Canvas.SetZIndex(HalfRight, 5);
			HasLeft = false;
			HasRight = false;
		}

		public void InitializeEvent()
		{

		}

		private void OnHover(object sender, MouseEventArgs e)
		{

		}

		/*
		private async void OnUnHover(object sender, MouseEventArgs e)
		{

		}
		*/



		public void GenerateHalfNode()
		{
			if (HasLeft)
			{
				HalfLeft.SetStatus(false);
			}
			else
			{
				HalfLeft.SetStatus(true);
			}

			if (HasRight)
			{
				HalfRight.SetStatus(false);
			}
			else
			{
				HalfRight.SetStatus(true);
			}
		}
	}
}
