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
    /// Логика взаимодействия для EditRouteWindow.xaml
    /// </summary>
    public partial class EditRouteWindow : Window
    {
        public EditRouteWindow()
        {
            InitializeComponent();
        }
        #region Properties
        public string Destination
        {
            get { return (string)GetValue(DestinationProperty); }
            set { SetValue(DestinationProperty, value); }
        }
        public static readonly DependencyProperty DestinationProperty =
            DependencyProperty.Register("Destination", typeof(string),
            typeof(EditRouteWindow), new
            PropertyMetadata(default(string)));
        public int Distance
        {
            get { return (int)GetValue(DistanceProperty); }
            set { SetValue(DistanceProperty, value); }
        }
        public static readonly DependencyProperty DistanceProperty =
            DependencyProperty.Register("Distance", typeof(int),
            typeof(EditRouteWindow), new
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
