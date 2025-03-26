using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using WpfAppCourse.Business.Infrastructure;
using WpfAppCourse.Business.Managers;
using WpfAppCourse.Commands;
using WpfAppCourse.Domain.Entities;

namespace WpfAppCourse
{
    /// <summary>
    /// Логика взаимодействия для EditApplicationWindow.xaml
    /// </summary>
    public partial class EditApplicationWindow : Window
    {
        ManagersFactory factory;
        RouteManager routeManager;
        public ObservableCollection<Route> Routes { get; set; }

        public EditApplicationWindow()
        {
            InitializeComponent();
            factory = new ManagersFactory("DefaultConnection");
            routeManager = factory.GetRouteManager();
            Routes = new ObservableCollection<Route>(routeManager.GetAllRoutes());
        }
        #region Properties

        public string CargoName
        {
            get { return (string)GetValue(CargoNameProperty); }
            set { SetValue(CargoNameProperty, value); }
        }
        public static readonly DependencyProperty CargoNameProperty =
            DependencyProperty.Register("CargoName", typeof(string),
            typeof(EditApplicationWindow), new
            PropertyMetadata(default(string)));
        public DateTime DateOfDispatch
        {
            get { return (DateTime)GetValue(DateOfDispatchProperty); }
            set { SetValue(DateOfDispatchProperty, value); }
        }
        public static readonly DependencyProperty DateOfDispatchProperty =
            DependencyProperty.Register("DateOfDispatch", typeof(DateTime),
            typeof(EditApplicationWindow), new
            PropertyMetadata(default(DateTime)));

        public int CargoWeight
        {
            get { return (int)GetValue(CargoWeightProperty); }
            set { SetValue(CargoWeightProperty, value); }
        }
        public static readonly DependencyProperty CargoWeightProperty =
            DependencyProperty.Register("CargoWeight", typeof(int),
            typeof(EditApplicationWindow), new
            PropertyMetadata(default(int)));

        public string Destination
        {
            get { return (string)GetValue(DestinationProperty); }
            set { SetValue(DestinationProperty, value); }
        }
        public static readonly DependencyProperty DestinationProperty =
            DependencyProperty.Register("Destination", typeof(string),
            typeof(EditApplicationWindow), new
            PropertyMetadata(default(string)));
        //public string Route
        //{
        //    get { return (string)GetValue(DestinationProperty); }
        //    set { SetValue(DestinationProperty, value); }
        //}
        //public static readonly DependencyProperty DestinationProperty =
        //    DependencyProperty.Register("Destination", typeof(string),
        //    typeof(EditApplicationWindow), new
        //    PropertyMetadata(default(string)));

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
