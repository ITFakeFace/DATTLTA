using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeManagementApplication.Model.VisualModel
{
	internal class GridCoordinate
	{
		public int X { get; set; }
		public int Y { get; set; }
		public GridCoordinate(int X, int Y)
		{
			this.X = X;
			this.Y = Y;
		}
	}
}
