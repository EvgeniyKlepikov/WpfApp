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
        //DriverManager driverManager;
        private string title = "Автомобили";
        public ObservableCollection<Car> Cars { get; set; }
        //public ObservableCollection<Application> Applications { get; set; }
        //public ObservableCollection<Driver> Drivers { get; set; }
        public string Title { get => title; set => title = value; }
        private Car _selectedCar;

        public CarWindowViewModel()
        {
            factory = new ManagersFactory("DefaultConnection");
            carManager = factory.GetCarManager();
            //if (carManager.Cars.Count() == 0)
            //    DbTestData.SetupData(carManager);
            //applicationManager = factory.GetApplicationManager();
            //driverManager = factory.GetDriverManager();
            Cars = new ObservableCollection<Car>(carManager.Cars);
            //Applications = new ObservableCollection<Application>();
            //Drivers = new ObservableCollection<Driver>();
            //if (Cars.Count() > 0)
            //    OnGetDriverExecuted(Cars[0].CarId);
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
        //private ICommand _getCarsCommand;
        //public ICommand GetCarsCommand
        //    => _getCarsCommand
        //    ??= new RelayCommand(OnGetCarExecuted);
        //private void OnGetCarExecuted(object id)
        //{
        //    Cars.Clear();
        //    var cars = carManager.GetDriversOfCar((int)id);
        //    foreach (var driver in drivers)
        //        Drivers.Add(driver);
        //}
        #endregion Commands
        #region Car
        private ICommand _newCarCommand;
        public ICommand NewCarCommand =>
        _newCarCommand ??= new
        RelayCommand(OnNewCarExecuted);
        private void OnNewCarExecuted(object id)
        {
            var dialog = new EditCarWindow
            {
                //DateOfAdmission = DateTime.Now
            };
            if (dialog.ShowDialog() != true) return;
            var car = new Car
            {
                CarName = dialog.CarName,
                CarNumber = dialog.CarNumber,
                CarWeight = dialog.CarWeight,
            };
            carManager.CreateCar(car);
            Cars.Add(car);
        }
        #endregion
        //#region Выбранный водитель
        //private Driver _selectedDriver;
        //public Driver SelectedDriver
        //{
        //    get => _selectedDriver;
        //    set
        //    {
        //        Set(ref _selectedDriver, value);
        //    }
        //}
        //#endregion
        #region Редактирование автомобиля
        private ICommand _editCarCommand;
        public ICommand EditCarCommand =>
        _editCarCommand ??=
        new RelayCommand(OnEditCarExecuted,
        EditCarCanExecute);
        // Проверка возможности редактирования
        private bool EditCarCanExecute(object p) =>

        _selectedCar != null;

        private void OnEditCarExecuted(object id)
        {
            var dialog = new EditCarWindow
            {
                CarName = _selectedCar.CarName,
                CarNumber = _selectedCar.CarNumber,
                CarWeight = _selectedCar.CarWeight,
            };
            if (dialog.ShowDialog() != true) return;
            // Сохранение параметров
            _selectedCar.CarName = dialog.CarName;
            _selectedCar.CarNumber = dialog.CarNumber;
            _selectedCar.CarWeight = dialog.CarWeight;
            carManager.UpdateCar(_selectedCar);
            // Обновить список заявок
            //OnGetCarExecuted(_selectedCar.CarId);
        }
        #endregion
        public ICommand _deleteCarCommand;

        public ICommand DeleteCarCommand
            => _deleteCarCommand
            ??= new RelayCommand(OnDeleteCarExecuted, DeleteCarCanExecute);
        // Проверка возможности удаления
        private bool DeleteCarCanExecute(object p) =>

        _selectedCar != null;


        private void OnDeleteCarExecuted(object ob)
        {
            if (SelectedCar != null)
            {
                var result = System.Windows.MessageBox.Show($"Автомобиль {SelectedCar.CarName} будет удален!", "Подтверждение", System.Windows.MessageBoxButton.YesNo);
                if (result == System.Windows.MessageBoxResult.Yes)
                {
                    carManager.DeleteCar(_selectedCar.CarId);
                    //applicationManager.UpdateApplication(_selectedApplication);
                    //OnGetCarExecuted(_selectedCar.CarId);
                }
            }
        }
    }
}
