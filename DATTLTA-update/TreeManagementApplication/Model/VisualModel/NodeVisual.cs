using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace TreeManagementApplication.Model.VisualModel
{
	internal class NodeVisual
	{
		public Ellipse Shape { get; set; }
		public String Text { get; set; }

		public NodeVisual()
		{
			Shape = new Ellipse
			{

			};
		}
	}
}
