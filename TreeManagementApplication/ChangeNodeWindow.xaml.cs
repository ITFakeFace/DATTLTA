using System.Windows;
using System.Windows.Input;

namespace TreeManagementApplication
{
    /// <summary>
    /// Interaction logic for ChangeNodeWindow.xaml
    /// </summary>
    public partial class ChangeNodeWindow : Window
    {
        public string? InpValue = null;
        public ChangeNodeWindow()
        {
            InitializeComponent();
            Loaded += InpChangeVal_GotFocus;
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
                    InpValue = InpChangeVal.Text.ToString();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            InpValue = null;
            this.Close();
        }

        private void OKBtn_Click(object sender, RoutedEventArgs e)
        {
            InpValue = InpChangeVal.Text.ToString();
            this.Close();
        }
    }
}
