using System.Data;
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

namespace WpfAppLaba7Adapter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DataTable personTable = new DataTable();
        public MainWindow()
        {
            InitializeComponent();
            Fill();
        }
        private void Fill()
        {
            personTable.Rows.Clear();
            personTable = Person.ViewAll();
            personGrid.ItemsSource = personTable.DefaultView;
        }

        private void ButtonView_Click(object sender, RoutedEventArgs e)
        {
            Fill();
        }

        private void ButtonUpdate_Click(object sender, RoutedEventArgs e)
        {
            Person.Update();
            Fill();
        }

        private void ButtonFind_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}