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

namespace WpfAppLaba6
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Intergral intergral;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void buttonParams_Click(object sender, RoutedEventArgs e)
        {
            Window1 window1 = new Window1();
            if (window1.ShowDialog() != true) return;
            intergral = window1.intergral;
            //MessageBox.Show(intergral.ToString());
        }

        private void buttonD_Click(object sender, RoutedEventArgs e)
        {
            Calculate();
        }

        private void buttonW_Click(object sender, RoutedEventArgs e)
        {

        }

        private void buttonA_Click(object sender, RoutedEventArgs e)
        {

        }
        private void Calculate()
        {
            if (intergral == null) return;
            int n = intergral.N;
            double h = (intergral.B - intergral.A) / n;
            double S = 0;
            for (int i = 0; i <= n; i++)
            {
                double x = intergral.A + h * i;
                S += intergral.func(x) * h;
            }
            MessageBox.Show($"S = {S:0.00000}");
        }
    }
}