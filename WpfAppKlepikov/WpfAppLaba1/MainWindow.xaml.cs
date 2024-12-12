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
        double x;
        char operation;
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
            x = Convert.ToDouble(textBox.Text);
            operation = (sender as Button).Content.ToString()[0];
            textBox.Text = "0";
        }

        private void ButtonEnter_Click(object sender, RoutedEventArgs e)
        {
            double y = Convert.ToDouble(textBox.Text);
            double result = 0;
            switch(operation)
            {
                case '+': result = x + y; break;
                case '-': result = x - y; break;
                case '*': result = x * y; break;
                case '/': result = x / y; break;

            }
            textBox.Text = result.ToString();
        }

        private void ButtonPoint_Click(object sender, RoutedEventArgs e)
        {
            if (textBox.Text.Length >= 17 | textBox.Text.Contains(','))
                return;
            textBox.Text += ",";
        }

        private void ButtonClear_Click(object sender, RoutedEventArgs e)
        {
            x = 0;
            operation = '\0';
            textBox.Text = "0";
        }
    }
}