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
    public class CarWindowViewModel : ViewModelBase
    {
        ManagersFactory factory;
        CarManager carManager;
        //ApplicationManager applicationManager;
        DriverManager driverManager;
        private string title = "Автомобили";
        public ObservableCollection<Car> Cars { get; set; }
        //public ObservableCollection<Application> Applications { get; set; }
        public ObservableCollection<Driver> Drivers { get; set; }
        public string Title { get => title; set => title = value; }
        private Car _selectedCar;

        public CarWindowViewModel()
        {
            factory = new ManagersFactory("DefaultConnection");
            carManager = factory.GetCarManager();
            //if (carManager.Cars.Count() == 0)
            //    DbTestData.SetupData(carManager);
            //applicationManager = factory.GetApplicationManager();
            driverManager = factory.GetDriverManager();
            Cars = new ObservableCollection<Car>(carManager.Cars);
            //Applications = new ObservableCollection<Application>();
            Drivers = new ObservableCollection<Driver>();
            if (Cars.Count() > 0)
                OnGetDriverExecuted(Cars[0].CarId);
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
        private ICommand _getDriversCommand;
        public ICommand GetDriversCommand
            => _getDriversCommand
            ??= new RelayCommand(OnGetDriverExecuted);
        private void OnGetDriverExecuted(object id)
        {
            Drivers.Clear();
            var drivers = carManager.GetDriversOfCar((int)id);
            foreach (var driver in drivers)
                Drivers.Add(driver);
        }
        #endregion Commands
        #region AddAplication
        private ICommand _newDriverCommand;
        public ICommand NewDriverCommand =>
        _newDriverCommand ??= new
        RelayCommand(OnNewDriverExecuted);
        private void OnNewDriverExecuted(object id)
        {
            var dialog = new EditDriverWindow
            {
                DateOfAdmission = DateTime.Now
            };
            if (dialog.ShowDialog() != true) return;
            var driver = new Driver
            {
                DriverName = dialog.DriverName,
                //DriverSurname = dialog.DriverSurname,
                DriverAge = dialog.DriverAge,
                //DriverExperience = dialog.DriverExperience,
                DateOfAdmission = dialog.DateOfAdmission,
            };
            carManager.AddDriverToCar(driver,
            _selectedCar.CarId);
            Drivers.Add(driver);
        }
        #endregion
        #region Выбранный водитель
        private Driver _selectedDriver;
        public Driver SelectedDriver
        {
            get => _selectedDriver;
            set
            {
                Set(ref _selectedDriver, value);
            }
        }
        #endregion
        #region Редактирование водителя
        private ICommand _editDriverCommand;
        public ICommand EditDriverCommand =>
        _editDriverCommand ??=
        new RelayCommand(OnEditDriverExecuted,
        EditDriverCanExecute);
        // Проверка возможности редактирования
        private bool EditDriverCanExecute(object p) =>

        _selectedDriver != null;

        private void OnEditDriverExecuted(object id)
        {
            var dialog = new EditDriverWindow
            {
                DriverName = _selectedDriver.DriverName,
                //DriverSurname = _selectedDriver.DriverSurname,
                DriverAge = _selectedDriver.DriverAge,
                //DriverExperience = _selectedDriver.DriverExperience,
                DateOfAdmission = _selectedDriver.DateOfAdmission,
            };
            if (dialog.ShowDialog() != true) return;
            // Сохранение параметров
            _selectedDriver.DriverName = dialog.DriverName;
            //_selectedDriver.DriverSurname = dialog.DriverSurname;
            _selectedDriver.DriverAge = dialog.DriverAge;
            //_selectedDriver.DriverExperience = dialog.DriverExperience;
            _selectedDriver.DateOfAdmission = dialog.DateOfAdmission;
            driverManager.UpdateDriver(_selectedDriver);
            // Обновить список заявок
            OnGetDriverExecuted(_selectedCar.CarId);
        }
        #endregion
        public ICommand _deleteDriverCommand;

        public ICommand DeleteDriverCommand
            => _deleteDriverCommand
            ??= new RelayCommand(OnDeleteDriverExecuted, DeleteDriverCanExecute);
        // Проверка возможности удаления
        private bool DeleteDriverCanExecute(object p) =>

        _selectedDriver != null;


        private void OnDeleteDriverExecuted(object ob)
        {
            if (SelectedDriver != null)
            {
                var result = System.Windows.MessageBox.Show($"Водитель {SelectedDriver.DriverSurname} будет удален!", "Подтверждение", System.Windows.MessageBoxButton.YesNo);
                if (result == System.Windows.MessageBoxResult.Yes)
                {
                    driverManager.DeleteDriver(_selectedDriver.DriverId);
                    //applicationManager.UpdateApplication(_selectedApplication);
                    OnGetDriverExecuted(_selectedDriver.CarId);
                }
            }
        }
    }
}
