using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfAppLaba1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonNumber_Click(object sender, RoutedEventArgs e)
        {
            if(textBox.Text == "0")
                textBox.Text = (sender as Button)?.Content.ToString();
            else
                if(textBox.Text.Length < 17)
                textBox.Text += (sender as Button)?.Content.ToString();
        }

        private void ButtonOperation_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}