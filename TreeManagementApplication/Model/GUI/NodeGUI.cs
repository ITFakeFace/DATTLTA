using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;

namespace TreeManagementApplication.Model.VisualModel
{
	internal class NodeGUI
	{
		public Ellipse Shape { get; set; }
		public String Text { get; set; }
		public Coordinate Coordinate { get; set; }

		public NodeGUI()
		{
			this.Shape = new Ellipse
			{
				Width = 50,
				Height = 50,
				Fill = Brushes.White,
				Stroke = Brushes.Black,
				StrokeThickness = 2,
			};

			this.Text = "null";
		}

		public NodeGUI(String text, Coordinate coordinate)
		{
			this.Shape = new Ellipse
			{
				Width = 50,
				Height = 50,
				Fill = Brushes.White,
				Stroke = Brushes.Black,
				StrokeThickness = 2,
			};
			this.Text = text;
			this.Coordinate = coordinate;
		}
	}
}
