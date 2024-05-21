using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace TreeManagementApplication.UserControls.Converter
{
	internal class HalfNodeTypeConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			//NodeType type = DesignerProperties.GetIsInDesignMode(this);
			if (value is NodeType nodeType)
			{
				switch (nodeType)
				{
					case NodeType.Left:
						return new CornerRadius(37.5, 0, 0, 37.5);
					case NodeType.Right:
						return new CornerRadius(0, 37.5, 37.5, 0);
					default:
						return new CornerRadius(0);
				}
			}

			return new CornerRadius(0);
		}
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}

}
