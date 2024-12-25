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
    public class MainWindowViewModel : ViewModelBase
    {
        ManagersFactory factory;
        CarManager carManager;
        ApplicationManager applicationManager;
        private string title = "Cars Window";
        public ObservableCollection<Car> Cars { get; set; }
        public ObservableCollection<Application> Applications { get; set; }
        public string Title { get => title; set => title = value; }
        private Car _selectedCar;

        public MainWindowViewModel()
        {
            factory = new ManagersFactory("DefaultConnection");
            carManager = factory.GetCarManager();
            if (carManager.Cars.Count() == 0)
                DbTestData.SetupData(carManager);
            applicationManager = factory.GetApplicationManager();
            Cars = new ObservableCollection<Car>(carManager.Cars);
            Applications = new ObservableCollection<Application>();
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
    }
}
