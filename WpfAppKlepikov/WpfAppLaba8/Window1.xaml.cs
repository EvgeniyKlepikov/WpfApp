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
using WpfAppLaba8.Models;

namespace WpfAppLaba8
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        Student student;
        public Window1()
        {
            InitializeComponent();
        }
        public Window1(Student student):this()
        {
            this.student = student;
            grid.DataContext = student;
        }

        private void ButtonOK_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
