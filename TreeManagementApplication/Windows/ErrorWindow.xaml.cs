using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;

namespace TreeManagementApplication.Windows
{
    /// <summary>
    /// Interaction logic for ErrorWindow.xaml
    /// </summary>
    public partial class ErrorWindow : Window
    {
        public int _status;
        public ErrorWindow(string text)
        {
            InitializeComponent();
            MainContent.Text = text;
        }



        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            _status = 201;
            this.Close();
        }

        private void BtnCancel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _status = 202;
            this.Close();
        }
    }
}
