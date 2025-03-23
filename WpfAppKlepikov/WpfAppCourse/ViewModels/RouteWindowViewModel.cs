using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfAppCourse.Business.Infrastructure;
using WpfAppCourse.Business.Managers;
using WpfAppCourse.Commands;
using WpfAppCourse.Domain.Entities;

namespace WpfAppCourse.ViewModels
{
    public class RouteWindowViewModel : ViewModelBase
    {
        ManagersFactory factory;
        CarManager carManager;
        RouteManager routeManager;
        private string title = "Маршруты";
        public ObservableCollection<Car> Cars { get; set; }
        //public ObservableCollection<Application> Applications { get; set; }
        public ObservableCollection<Route> Routes { get; set; }
        public string Title { get => title; set => title = value; }
        private Car _selectedCar;

        public RouteWindowViewModel()
        {
            factory = new ManagersFactory("DefaultConnection");
            carManager = factory.GetCarManager();
            //if (carManager.Cars.Count() == 0)
            //    DbTestData.SetupData(carManager);
            //applicationManager = factory.GetApplicationManager();
            routeManager = factory.GetRouteManager();
            Cars = new ObservableCollection<Car>(carManager.Cars);
            //Applications = new ObservableCollection<Application>();
            Routes = new ObservableCollection<Route>();
            if (Cars.Count() > 0)
                OnGetRouteExecuted(Cars[0].CarId);
        }

        public Car SelectedCar
        {
            get => _selectedCar;
            set
            {
                Set(ref _selectedCar, value);
            }
        }
        #region Commands
        private ICommand _getRoutesCommand;
        public ICommand GetRoutesCommand
            => _getRoutesCommand
            ??= new RelayCommand(OnGetRouteExecuted);
        private void OnGetRouteExecuted(object id)
        {
            Routes.Clear();
            var routes = carManager.GetRoutesOfCar((int)id);
            foreach (var route in routes)
                Routes.Add(route);
        }
        #endregion Commands
        #region Route
        private ICommand _newRouteCommand;
        public ICommand NewRouteCommand =>
        _newRouteCommand ??= new
        RelayCommand(OnNewRouteExecuted);
        private void OnNewRouteExecuted(object id)
        {
            var dialog = new EditRouteWindow
            {
            };
            if (dialog.ShowDialog() != true) return;
            var route = new Route
            {
                Destination = dialog.Destination,
                Distance = dialog.Distance,
            };
            carManager.AddRouteToCar(route, _selectedCar.CarId);
            Routes.Add(route);
        }
        #endregion
        #region Выбранный маршрут
        private Route _selectedRoute;
        public Route SelectedRoute
        {
            get => _selectedRoute;
            set
            {
                Set(ref _selectedRoute, value);
            }
        }
        #endregion
        #region Редактирование маршрута
        private ICommand _editRouteCommand;
        public ICommand EditRouteCommand =>
        _editRouteCommand ??=
        new RelayCommand(OnEditRouteExecuted,
        EditRouteCanExecute);
        // Проверка возможности редактирования
        private bool EditRouteCanExecute(object p) =>

        _selectedRoute != null;

        private void OnEditRouteExecuted(object id)
        {
            var dialog = new EditRouteWindow
            {
                Destination = _selectedRoute.Destination,
                Distance = _selectedRoute.Distance,
            };
            if (dialog.ShowDialog() != true) return;
            // Сохранение параметров
            _selectedRoute.Destination = dialog.Destination;
            _selectedRoute.Distance = dialog.Distance;
            routeManager.UpdateRoute(_selectedRoute);
            // Обновить список заявок
            OnGetRouteExecuted(_selectedCar.CarId);
        }
        #endregion
        public ICommand _deleteRouteCommand;

        public ICommand DeleteRouteCommand
            => _deleteRouteCommand
            ??= new RelayCommand(OnDeleteRouteExecuted, DeleteRouteCanExecute);
        // Проверка возможности удаления
        private bool DeleteRouteCanExecute(object p) =>

        _selectedRoute != null;


        private void OnDeleteRouteExecuted(object ob)
        {
            if (SelectedRoute != null)
            {
                var result = System.Windows.MessageBox.Show($"Маршрут {SelectedRoute.RouteId} будет удален!", "Подтверждение", System.Windows.MessageBoxButton.YesNo);
                if (result == System.Windows.MessageBoxResult.Yes)
                {
                    routeManager.DeleteRoute(_selectedRoute.RouteId);
                    //applicationManager.UpdateApplication(_selectedApplication);
                    OnGetRouteExecuted(_selectedRoute.CarId);
                }
            }
        }

    }
}
