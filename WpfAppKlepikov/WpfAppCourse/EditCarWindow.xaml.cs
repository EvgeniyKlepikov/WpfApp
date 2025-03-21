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
    /// Логика взаимодействия для EditCarWindow.xaml
    /// </summary>
    public partial class EditCarWindow : Window
    {
        public EditCarWindow()
        {
            InitializeComponent();
        }
        #region Properties
        public string CarName
        {
            get { return (string)GetValue(CarNameProperty); }
            set { SetValue(CarNameProperty, value); }
        }
        public static readonly DependencyProperty CarNameProperty =
            DependencyProperty.Register("CarName", typeof(string),
            typeof(EditCarWindow), new
            PropertyMetadata(default(string)));
        public int CarNumber
        {
            get { return (int)GetValue(CarNumberProperty); }
            set { SetValue(CarNumberProperty, value); }
        }
        public static readonly DependencyProperty CarNumberProperty =
            DependencyProperty.Register("CarNumber", typeof(int),
            typeof(EditCarWindow), new
            PropertyMetadata(default(int)));

        public int CarWeight
        {
            get { return (int)GetValue(CarWeightProperty); }
            set { SetValue(CarWeightProperty, value); }
        }
        public static readonly DependencyProperty CarWeightProperty =
            DependencyProperty.Register("CarWeight", typeof(int),
            typeof(EditCarWindow), new
            PropertyMetadata(default(int)));
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
