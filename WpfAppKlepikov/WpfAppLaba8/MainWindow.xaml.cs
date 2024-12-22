using System;
using System.Collections.Generic;
using System.Data.Entity;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfAppLaba8.Models;

namespace WpfAppLaba8
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        EntityContext entityContext;
        public MainWindow()
        {
            InitializeComponent();
            entityContext = new EntityContext();
            entityContext.Students.Load();
            dGrid.ItemsSource = entityContext.Students.Local.ToBindingList();

        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dGrid.SelectedItems.Count == 0) return;
            for (int i = dGrid.SelectedItems.Count - 1; i >= 0; i--)
            {
                Student student = dGrid.SelectedItems[i] as Student;
                if(student != null)
                    entityContext.Students.Remove(student);
            }
            entityContext.SaveChanges();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            entityContext.SaveChanges();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            entityContext.Dispose();
        }
    }
}
