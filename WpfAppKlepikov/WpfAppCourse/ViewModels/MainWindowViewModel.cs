using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
    public class MainWindowViewModel : ViewModelBase
    {
        ManagersFactory factory;
        CarManager carManager;
        ApplicationManager applicationManager;
        DriverManager driverManager;
        private string title = "Грузоперевозки";
        public ObservableCollection<Car> Cars { get; set; }
        public ObservableCollection<Application> Applications { get; set; }
        public ObservableCollection<Driver> Drivers { get; set; }
        public string Title { get => title; set => title = value; }
        private Car _selectedCar;

        public MainWindowViewModel()
        {
            factory = new ManagersFactory("DefaultConnection");
            carManager = factory.GetCarManager();
            if (carManager.Cars.Count() == 0)
                DbTestData.SetupData(carManager);
            applicationManager = factory.GetApplicationManager();
            driverManager = factory.GetDriverManager();
            Cars = new ObservableCollection<Car>(carManager.Cars);
            Applications = new ObservableCollection<Application>();
            Drivers = new ObservableCollection<Driver>();
            if (Cars.Count() > 0)
                OnGetApplicationExecuted(Cars[0].CarId);
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
        private ICommand _getApplicationsCommand;
        public ICommand GetApplicationsCommand 
            => _getApplicationsCommand
            ??= new RelayCommand(OnGetApplicationExecuted);
        private void OnGetApplicationExecuted(object id)
        {
            Applications.Clear();
            var applications = carManager.GetApplicationsOfCar((int)id);
            foreach (var application in applications)
                Applications.Add(application);
        }
        #endregion Commands
        #region AddAplication
        private ICommand _newApplicationCommand;
        public ICommand NewApplicationCommand =>
        _newApplicationCommand ??= new
        RelayCommand(OnNewApplicationExecuted);
        private void OnNewApplicationExecuted(object id)
        {
            var dialog = new EditApplicationWindow
            {
                DateOfDispatch = DateTime.Now
            };
            if (dialog.ShowDialog() != true) return;
            var application = new Application
            {
                CargoName = dialog.CargoName,
                DateOfDispatch = dialog.DateOfDispatch,
                Destination = dialog.Destination,
                CargoWeight = dialog.CargoWeight,
            };
            carManager.AddApplicationToCar(application,
            _selectedCar.CarId);
            Applications.Add(application);
        }
        #endregion
        #region Выбранная заявка
        private Application _selectedApplication;
        public Application SelectedApplication
        {
            get => _selectedApplication;
            set
            {
                Set(ref _selectedApplication, value);
            }
        }
        #endregion
        #region Редактирование заявки
        private ICommand _editApplicationCommand;
        public ICommand EditApplicationCommand =>
        _editApplicationCommand ??=
        new RelayCommand(OnEditApplicationExecuted,
        EditApplicationCanExecute);
        // Проверка возможности редактирования
        private bool EditApplicationCanExecute(object p) =>

        _selectedApplication != null;

        private void OnEditApplicationExecuted(object id)
        {
            var dialog = new EditApplicationWindow
            {
                CargoName = _selectedApplication.CargoName,
                DateOfDispatch = _selectedApplication.DateOfDispatch,
                Destination = _selectedApplication.Destination,
                CargoWeight = _selectedApplication.CargoWeight

            };
            if (dialog.ShowDialog() != true) return;
            // Сохранение параметров
            _selectedApplication.CargoName = dialog.CargoName;
            _selectedApplication.DateOfDispatch = dialog.DateOfDispatch;
            _selectedApplication.Destination = dialog.Destination;
            _selectedApplication.CargoWeight = dialog.CargoWeight;
            applicationManager.UpdateApplication(_selectedApplication);
            // Обновить список заявок
            OnGetApplicationExecuted(_selectedCar.CarId);
        }
        #endregion
        public ICommand _deleteApplicationCommand;

        public ICommand DeleteApplicationCommand
            => _deleteApplicationCommand
            ??= new RelayCommand(OnDeleteApplicationExecuted, DeleteApplicationCanExecute);
        // Проверка возможности удаления
        private bool DeleteApplicationCanExecute(object p) =>

        _selectedApplication != null;


        private void OnDeleteApplicationExecuted(object ob)
        {
            if (SelectedApplication != null)
            {
                var result = System.Windows.MessageBox.Show($"Заявка {SelectedApplication.CargoName} будет удалена!", "Подтверждение", System.Windows.MessageBoxButton.YesNo);
                if (result == System.Windows.MessageBoxResult.Yes)
                {
                    applicationManager.DeleteApplication(_selectedApplication.ApplicationId);
                    //applicationManager.UpdateApplication(_selectedApplication);
                    OnGetApplicationExecuted(_selectedCar.CarId);

                }
            }
        }
        #region DriverWindow
        private ICommand _driverWindowCommand;
        public ICommand DriverWindowCommand =>
        _driverWindowCommand ??= new
        RelayCommand(OnDriverWindowExecuted);
        private void OnDriverWindowExecuted(object id)
        {
            var dialog = new DriverWindow
            {
            };
            if (dialog.ShowDialog() != true) return;
            //var application = new Application
            //{
            //    CargoName = dialog.CargoName,
            //    DateOfDispatch = dialog.DateOfDispatch,
            //    Destination = dialog.Destination,
            //    CargoWeight = dialog.CargoWeight,
            //};
            //carManager.AddApplicationToCar(application,
            //_selectedCar.CarId);
            //Applications.Add(application);
        }
        #endregion
        #region CarWindow
        private ICommand _carWindowCommand;
        public ICommand CarWindowCommand =>
        _carWindowCommand ??= new
        RelayCommand(OnCarWindowExecuted);
        private void OnCarWindowExecuted(object id)
        {
            var dialog = new CarWindow
            {
            };
            if (dialog.ShowDialog() != true) return;
            //var application = new Application
            //{
            //    CargoName = dialog.CargoName,
            //    DateOfDispatch = dialog.DateOfDispatch,
            //    Destination = dialog.Destination,
            //    CargoWeight = dialog.CargoWeight,
            //};
            //carManager.AddApplicationToCar(application,
            //_selectedCar.CarId);
            //Applications.Add(application);
        }
        #endregion
        #region ClientWindow
        private ICommand _clientWindowCommand;
        public ICommand ClientWindowCommand =>
        _clientWindowCommand ??= new
        RelayCommand(OnClientWindowExecuted);
        private void OnClientWindowExecuted(object id)
        {
            var dialog = new ClientWindow
            {
            };
            if (dialog.ShowDialog() != true) return;
            //var application = new Application
            //{
            //    CargoName = dialog.CargoName,
            //    DateOfDispatch = dialog.DateOfDispatch,
            //    Destination = dialog.Destination,
            //    CargoWeight = dialog.CargoWeight,
            //};
            //carManager.AddApplicationToCar(application,
            //_selectedCar.CarId);
            //Applications.Add(application);
        }
        #endregion

    }
}
