using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeManagementApplication.Model.System
{
	internal class Coordinate
	{
		public double X { get; set; } = 0;
		public double Y { get; set; } = 0;

		public Coordinate(double X, double Y)
		{
			this.X = X;
			this.Y = Y;
		}
	}
}
