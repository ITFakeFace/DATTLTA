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
using System.Windows.Shapes;

namespace TreeManagementApplication.Windows
{
    /// <summary>
    /// Interaction logic for BooleanMessageWindow.xaml
    /// </summary>
    public partial class BooleanMessageWindow : Window
    {
        private bool Value = false;
        public bool IsClicked = false;
        public BooleanMessageWindow()
        {
            InitializeComponent();
            InitializeProperty();
            InitializeEvent();
        }

        public void InitializeProperty()
        {

        }

        public void InitializeEvent()
        {

        }

        public async Task<bool> GetValue()
        {
            return this.Value;
        }

        private void BtnYes_MouseDown(object sender, RoutedEventArgs e)
        {
            Value = true;
            IsClicked = true;
            this.Close();
        }

        private void BtnNo_MouseDown(object sender, RoutedEventArgs e)
        {
            Value = false;
            IsClicked = true;
            this.Close();
        }

    }
}
