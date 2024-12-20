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

namespace WpfAppLaba5
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Shape shape;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MenuItemShape_Click(object sender, RoutedEventArgs e)
        {
            WindowShape windowShape = new WindowShape();
            if(windowShape.ShowDialog() == false) return;
            shape = windowShape.GetShape();
            //MessageBox.Show(shape.ToString());
        }

        private void canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(shape == null) return;
            shape.Draw(canvas, e.GetPosition(canvas));
        }
    }
}