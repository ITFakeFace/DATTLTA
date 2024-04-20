using System.Windows;
using System.Windows.Input;

namespace TreeManagementApplication
{
    /// <summary>
    /// Interaction logic for ChangeNodeWindow.xaml
    /// </summary>
    public partial class ChangeNodeWindow : Window
    {
        public string value;
        public ChangeNodeWindow()
        {
            InitializeComponent();

        }

        private void InpChangeVal_GotFocus(object sender, RoutedEventArgs e)
        {
            if (InpChangeVal.Text.ToLower().Equals("value"))
            {
                InpChangeVal.Text = "";
            }
        }
        private void InpChangeVal_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Enter)
                {
                    value = InpChangeVal.Text.ToString();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            value = "";
            this.Close();
        }

        private void OKBtn_Click(object sender, RoutedEventArgs e)
        {
            value = InpChangeVal.Text.ToString();
            this.Close();
        }
    }
}
