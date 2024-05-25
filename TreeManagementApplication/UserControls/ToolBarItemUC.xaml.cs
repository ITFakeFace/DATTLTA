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
	/// Interaction logic for ToolBarItemUC.xaml
	/// </summary>
	public enum ToolBarMode
	{
		Create, Update, Delete, Move, Save, Load, Select, Search, None
	}
	public partial class ToolBarItemUC : UserControl
	{
		public event EventHandler OnModeChange;
		public static readonly DependencyProperty ModeTypeProperty = DependencyProperty.Register("Mode", typeof(ToolBarMode), typeof(ToolBarItemUC), new PropertyMetadata(ToolBarMode.Create, ModeTypePropertyChanged));

		public ToolBarMode Mode
		{
			get { return (ToolBarMode)GetValue(ModeTypeProperty); }
			set { SetValue(ModeTypeProperty, value); }
		}

		public static void ModeTypePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			var control = (ToolBarItemUC)d;
			control.UpdateItemImage();
		}
		public bool isActive = false;

		public void UpdateItemImage()
		{
			String ImagePath = "pack://application:,,,/Resources/Static/Images/NavBar/Add_100px.png";
			if (Mode == ToolBarMode.Create)
			{
				ImagePath = "pack://application:,,,/Resources/Static/Images/NavBar/Add_100px.png";
			}
			else if (Mode == ToolBarMode.Update)
			{
				ImagePath = "pack://application:,,,/Resources/Static/Images/NavBar/Edit_100px.png";
			}
			else if (Mode == ToolBarMode.Delete)
			{
				ImagePath = "pack://application:,,,/Resources/Static/Images/NavBar/Remove_100px.png";
			}
			else if (Mode == ToolBarMode.Move)
			{
				ImagePath = "pack://application:,,,/Resources/Static/Images/NavBar/Expand_100px.png";
			}
			else if (Mode == ToolBarMode.Search)
			{
				ImagePath = "pack://application:,,,/Resources/Static/Images/NavBar/Search_100px.png";
			}
			else if (Mode == ToolBarMode.Save)
			{
				ImagePath = "pack://application:,,,/Resources/Static/Images/NavBar/Save_100px.png";
			}
			else
			{
				ImagePath = "pack://application:,,,/Resources/Static/Images/NavBar/Error_100px.png";
			}
			ItemImage.Source = new BitmapImage(new Uri(ImagePath));
		}
		public ToolBarItemUC()
		{
			InitializeComponent();
			InitializeEvent();
		}

		public void InitializeEvent()
		{
			MouseDown += OnClick;
		}

		public void OnClick(object sender, MouseEventArgs e)
		{
			if (!isActive)
			{
				Enable();
			}
			else
			{
				Disable();
			}
			if (this.OnModeChange != null)
				this.OnModeChange(this, new EventArgs());
		}

		public void Enable()
		{
			isActive = true;
			DisableAll();
			MainWindow.ModeMap[Mode].isActive = true;
			//RadialGradientBrush GradientBrush = new RadialGradientBrush();
			//GradientBrush.GradientStops.Add(new GradientStop(Colors.Blue, 1));
			//GradientBrush.GradientStops.Add(new GradientStop(Colors.Pink, 1));
			LinearGradientBrush linearGradientBrush = new LinearGradientBrush();
			//linearGradientBrush.GradientStops.Add(new GradientStop((Color)ColorConverter.ConvertFromString("#12c2e9"), 0.2));
			//linearGradientBrush.GradientStops.Add(new GradientStop((Color)ColorConverter.ConvertFromString("#c471ed"), 0.6));
			//linearGradientBrush.GradientStops.Add(new GradientStop((Color)ColorConverter.ConvertFromString("#f64f59"), 1));
			linearGradientBrush.GradientStops.Add(new GradientStop((Color)ColorConverter.ConvertFromString("#00d2ff"), 0.3));
			linearGradientBrush.GradientStops.Add(new GradientStop((Color)ColorConverter.ConvertFromString("#928DAB"), 1));
			ItemBorder.Background = linearGradientBrush;
		}
		public void Disable()
		{
			isActive = false;
			MainWindow.ModeMap[Mode].isActive = false;
			SolidColorBrush SolidBrush = new SolidColorBrush();
			SolidBrush.Color = Colors.White;
			ItemBorder.Background = SolidBrush;
		}

		public static void DisableAll()
		{
			foreach (ToolBarMode Mode in MainWindow.ModeMap.Keys)
			{
				MainWindow.ModeMap[Mode].isActive = false;
				MainWindow.ModeMap[Mode].Disable();
			}
		}
	}
}
