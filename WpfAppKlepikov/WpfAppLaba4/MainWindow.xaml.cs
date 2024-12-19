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

namespace WpfAppLaba4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Canvas[] canvases = new Canvas[3];
        public MainWindow()
        {
            InitializeComponent();
            this.ResizeMode = ResizeMode.NoResize;
            for (int i = 0; i < canvases.Length; i++)
            {
                canvases[i] = new Canvas();
                Grid.SetRow(canvases[i], i);
                grid.Children.Add(canvases[i]);
            }
        }
    }
}