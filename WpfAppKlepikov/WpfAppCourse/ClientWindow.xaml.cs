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

namespace WpfAppCourse
{
    /// <summary>
    /// Логика взаимодействия для ClientWindow.xaml
    /// </summary>
    public partial class ClientWindow : Window
    {
        public ClientWindow()
        {
            InitializeComponent();
        }
        //public int Deposit
        //{
        //    get { return (int)GetValue(DepositProperty); }
        //    set { SetValue(DepositProperty, value); }
        //}
        //public static readonly DependencyProperty DepositProperty =
        //    DependencyProperty.Register("Deposit", typeof(int),
        //    typeof(ClientWindow), new
        //    PropertyMetadata(default(int)));

    }
}
