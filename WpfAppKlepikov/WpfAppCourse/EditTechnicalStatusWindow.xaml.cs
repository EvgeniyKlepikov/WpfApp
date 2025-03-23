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
    /// Логика взаимодействия для EditTechnicalStatusWindow.xaml
    /// </summary>
    public partial class EditTechnicalStatusWindow : Window
    {
        public EditTechnicalStatusWindow()
        {
            InitializeComponent();
        }
        #region Properties
        public DateTime DateOfStatus
        {
            get { return (DateTime)GetValue(DateOfStatusProperty); }
            set { SetValue(DateOfStatusProperty, value); }
        }
        public static readonly DependencyProperty DateOfStatusProperty =
            DependencyProperty.Register("DateOfStatus", typeof(DateTime),
            typeof(EditTechnicalStatusWindow), new
            PropertyMetadata(default(DateTime)));
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
