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
    /// Логика взаимодействия для EditDriverWindow.xaml
    /// </summary>
    public partial class EditDriverWindow : Window
    {
        public EditDriverWindow()
        {
            InitializeComponent();
        }
        #region Properties
        public string DriverName
        {
            get { return (string)GetValue(DriverNameProperty); }
            set { SetValue(DriverNameProperty, value); }
        }
        public static readonly DependencyProperty DriverNameProperty =
            DependencyProperty.Register("DriverName", typeof(string),
            typeof(EditDriverWindow), new
            PropertyMetadata(default(string)));
        public string DriverSurname
        {
            get { return (string)GetValue(DriverSurnameProperty); }
            set { SetValue(DriverSurnameProperty, value); }
        }
        public static readonly DependencyProperty DriverSurnameProperty =
            DependencyProperty.Register("DriverSurname", typeof(string),
            typeof(EditDriverWindow), new
            PropertyMetadata(default(string)));

        public DateTime DateOfAdmission
        {
            get { return (DateTime)GetValue(DateOfAdmissionProperty); }
            set { SetValue(DateOfAdmissionProperty, value); }
        }
        public static readonly DependencyProperty DateOfAdmissionProperty =
            DependencyProperty.Register("DateOfAdmission", typeof(DateTime),
            typeof(EditDriverWindow), new
            PropertyMetadata(default(DateTime)));

        public int DriverAge
        {
            get { return (int)GetValue(DriverAgeProperty); }
            set { SetValue(DriverAgeProperty, value); }
        }
        public static readonly DependencyProperty DriverAgeProperty =
            DependencyProperty.Register("DriverAge", typeof(int),
            typeof(EditDriverWindow), new
            PropertyMetadata(default(int)));

        public int DriverExperience
        {
            get { return (int)GetValue(DriverExperienceProperty); }
            set { SetValue(DriverExperienceProperty, value); }
        }
        public static readonly DependencyProperty DriverExperienceProperty =
            DependencyProperty.Register("DriverExperience", typeof(int),
            typeof(EditDriverWindow), new
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
