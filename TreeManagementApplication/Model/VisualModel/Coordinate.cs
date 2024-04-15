using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeManagementApplication.Model.VisualModel
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
		public override string ToString()
		{
			return $"(X:{X},Y:{Y})";
		}
	}
}
