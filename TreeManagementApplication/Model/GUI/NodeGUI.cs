using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;
using TreeManagementApplication.Model.Interface;

namespace TreeManagementApplication.Model.VisualModel
{
	internal class NodeGUI<T> where T : IComparable<T>
	{
		public Ellipse Shape { get; set; } = new Ellipse
		{
			Width = 50,
			Height = 50,
			Fill = Brushes.White,
			Stroke = Brushes.Black,
			StrokeThickness = 2,
		};
		public String Text { get; set; } = "null";
		public Coordinate Coordinate { get; set; } = new Coordinate(0, 0);
		public GridCoordinate GridCoordinate { get; set; } = new GridCoordinate(0, 0);
		public NodeGUI()
		{
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
