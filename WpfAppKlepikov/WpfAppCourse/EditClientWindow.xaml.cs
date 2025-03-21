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
using WpfAppCourse.Commands;

namespace WpfAppCourse
{
    /// <summary>
    /// Логика взаимодействия для EditClientWindow.xaml
    /// </summary>
    public partial class EditClientWindow : Window
    {
        public EditClientWindow()
        {
            InitializeComponent();
        }
        #region Properties
        public string ClientName
        {
            get { return (string)GetValue(ClientNameProperty); }
            set { SetValue(ClientNameProperty, value); }
        }
        public static readonly DependencyProperty ClientNameProperty =
            DependencyProperty.Register("ClientName", typeof(string),
            typeof(EditClientWindow), new
            PropertyMetadata(default(string)));
        public string ClientSurname
        {
            get { return (string)GetValue(ClientClientSurnameProperty); }
            set { SetValue(ClientClientSurnameProperty, value); }
        }
        public static readonly DependencyProperty ClientClientSurnameProperty =
            DependencyProperty.Register("ClientSurname", typeof(string),
            typeof(EditClientWindow), new
            PropertyMetadata(default(string)));
        public string Company
        {
            get { return (string)GetValue(CompanyProperty); }
            set { SetValue(CompanyProperty, value); }
        }
        public static readonly DependencyProperty CompanyProperty =
            DependencyProperty.Register("Company", typeof(string),
            typeof(EditClientWindow), new
            PropertyMetadata(default(string)));
        #endregion

        private ICommand _okCommand;
        public ICommand OkCommand =>
        _okCommand
        ?? new RelayCommand(OnOkExecuted);
        public void OnOkExecuted(object param)
        {
            this.DialogResult = true;
            this.Close();
        }

    }
}
