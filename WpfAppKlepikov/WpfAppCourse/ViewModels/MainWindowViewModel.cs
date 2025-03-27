using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    public class MainWindowViewModel : ViewModelBase,INotifyPropertyChanged
    {
        ManagersFactory factory;
        CarManager carManager;
        ApplicationManager applicationManager;
        DriverManager driverManager;
        ClientManager clientManager;
        RouteManager routeManager;


        private string title = "Грузоперевозки";
        public ObservableCollection<Car> Cars { get; set; }
        public ObservableCollection<Application> Applications { get; set; }
        public ObservableCollection<Driver> Drivers { get; set; }
        public ObservableCollection<Client> Clients { get; set; }
        public ObservableCollection<Route> Routes { get; set; }

        public string Title { get => title; set => title = value; }
        private Car _selectedCar;

        public MainWindowViewModel()
        {
            factory = new ManagersFactory("DefaultConnection");
            carManager = factory.GetCarManager();
            clientManager = factory.GetClientManager();
            if (carManager.Cars.Count() == 0)
                DbTestData.SetupData(carManager, clientManager);
            applicationManager = factory.GetApplicationManager();
            driverManager = factory.GetDriverManager();
            routeManager = factory.GetRouteManager();
            Cars = new ObservableCollection<Car>(carManager.Cars);
            Applications = new ObservableCollection<Application>();
            Drivers = new ObservableCollection<Driver>();
            Clients = new ObservableCollection<Client>(clientManager.GetAllClients());
            Routes = new ObservableCollection<Route>(routeManager.GetAllRoutes());
            SelectedRoute = Routes.FirstOrDefault();
            SelectedClient = Clients.FirstOrDefault();
            SelectedDate = DateTime.Now.AddDays(1);
            if (Cars.Count() > 0)
                OnGetApplicationExecuted(Cars[0].CarId);
        }

        private int _weight;
        public int Weight
        {
            get => _weight;
            set
            {
                Set(ref _weight, value);
            }
        }
        private double _cost;
        public double Cost
        {
            get => _cost;
            set
            {
                Set(ref _cost, value);
            }
        }
        private double _transferInsurance;
        public double TransferInsurance
        {
            get => _transferInsurance;
            set
            {
                Set(ref _transferInsurance, value);
                //OnPropertyChanged(nameof(TransferInsurance));
                //OnPropertyChanged(nameof(Greeting1));
            }
        }
        private double _transferSum;
        public double TransferSum
        {
            get => _transferSum;
            set
            {
                Set(ref _transferSum, value);
                //OnPropertyChanged(nameof(TransferSum));
                //OnPropertyChanged(nameof(Greeting2));
            }
        }
        private double _transferTotalSum;
        public double TransferTotalSum
        {
            get => _transferTotalSum;
            set
            {
                Set(ref _transferTotalSum, value);
            }
        }
        private string _cargoTitle;
        public string CargoTitle
        {
            get => _cargoTitle;
            set
            {
                Set(ref _cargoTitle, value);
            }
        }

        //public event PropertyChangedEventHandler PropertyChanged;
        //protected virtual void OnPropertyChanged(string propertyName)
        //{
        //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        //}

        private Client _selectedClient;
        public Client SelectedClient
        {
            get => _selectedClient;
            set
            {
                Set(ref _selectedClient, value);
            }
        }
        //private double _clientSum;
        //public double ClientSum
        //{
        //    get => SelectedClient.ClientDeposit;
        //    set
        //    {
        //        //Set(SelectedClient.ClientDeposit, value);
        //    }
        //}
        private int _result1;
        public int Result1
        {
            get => _result1;
            set
            {
                Set(ref _result1, value);
            }
        }
        private int _result2;
        public int Result2
        {
            get => _result2;
            set
            {
                Set(ref _result2, value);
            }
        }

        private Route _selectedRoute;
        public Route SelectedRoute
        {
            get => _selectedRoute;
            set
            {
                Set(ref _selectedRoute, value);
            }
        }
        private DateTime _selectedDate;
        public DateTime SelectedDate
        {
            get => _selectedDate;
            set
            {
                Set(ref _selectedDate, value);
            }
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
        //#region Commands
        //private ICommand _getClientsCommand;
        //public ICommand GetClientsCommand
        //    => _getClientsCommand
        //    ??= new RelayCommand(OnGetClientsExecuted);
        //private void OnGetClientsExecuted(object id)
        //{
        //    Clients.Clear();
        //    var clients = clientManager.GetAllClients();
        //    foreach (var client in clients)
        //        Clients.Add(client);
        //}
        //#endregion Commands

        public string Greeting => $"Стоимость перевозки составляет {TransferSum} рублей\n" +
            $"Стоимость страхования составляет {TransferInsurance} рублей\n" +
            $"Общая сумма доставки {TransferTotalSum} рублей";
        public string Greeting2 => $"Для выбранной даты свободно {Result1} тонн";


        private ICommand _getCountOfTransferCommand;
        public ICommand GetCountOfTransferCommand
            => _getCountOfTransferCommand
            ??= new RelayCommand(OnCountOfTransferExecuted);
        private void OnCountOfTransferExecuted(object id)
        {
            TransferSum = SelectedRoute.Distance*0.6*Weight*0.8;
            TransferInsurance = Cost * 0.1;
            TransferTotalSum = TransferSum + TransferInsurance;
            OnPropertyChanged(nameof(TransferInsurance));
            OnPropertyChanged(nameof(TransferSum));
            OnPropertyChanged(nameof(Greeting));
        }

        private ICommand _getPaymentCommand;
        public ICommand GetPaymentCommand
            => _getPaymentCommand
            ??= new RelayCommand(OnGetPaymentExecuted);
        private void OnGetPaymentExecuted(object id)
        {
            if (SelectedClient.ClientDeposit < TransferTotalSum)
            {
                System.Windows.MessageBox.Show($"На балансе {SelectedClient.ClientSurname} недостаточно средств!", "Внимание!", System.Windows.MessageBoxButton.OK);
                return;
            }
            if (TransferTotalSum == 0) return;
            SelectedClient.ClientDeposit -= TransferTotalSum;
            clientManager.UpdateClient(SelectedClient);
            var payment = new Payment
            {
                TotalInsurance = TransferInsurance,
                TotalTrip = TransferSum,
                TotalSum = TransferTotalSum,
                DateOfRegistration = DateTime.Now,
            };
            clientManager.AddPaymentToClient(payment, _selectedClient.ClientId);
            //Applications.Add(application);
            System.Windows.MessageBox.Show($"Операция проведена успешна!", "Внимание!", System.Windows.MessageBoxButton.OK);

        }
        private ICommand _getPlaceCommand;
        public ICommand GetPlaceCommand
            => _getPlaceCommand
            ??= new RelayCommand(OnGetPlaceExecuted);
        private void OnGetPlaceExecuted(object id)
        {
            int result = 0;
            //foreach (V item in Application) { }
            //for (int i = 0;)
            var applications = applicationManager.GetAllApplications();
            foreach (var application in applications)
            {
                if ((SelectedDate == application.DateOfDispatch) && (SelectedRoute.CarId == application.CarId))
                {
                    result += application.CargoWeight;
                }
            }
            Result1 = 20 - result;
            //OnPropertyChanged(nameof(Result1));

            OnPropertyChanged(nameof(Greeting2));

            //if (SelectedClient.ClientDeposit < TransferTotalSum)
            //{
            //    System.Windows.MessageBox.Show($"На балансе {SelectedClient.ClientSurname} недостаточно средств!", "Внимание!", System.Windows.MessageBoxButton.OK);
            //    return;
            //}
            //if (TransferTotalSum == 0) return;
            //SelectedClient.ClientDeposit -= TransferTotalSum;
            //clientManager.UpdateClient(SelectedClient);
            //var payment = new Payment
            //{
            //    TotalInsurance = TransferInsurance,
            //    TotalTrip = TransferSum,
            //    TotalSum = TransferTotalSum,
            //    DateOfRegistration = DateTime.Now,
            //};
            //clientManager.AddPaymentToClient(payment, _selectedClient.ClientId);
            ////Applications.Add(application);
            //System.Windows.MessageBox.Show($"Операция проведена успешна!", "Внимание!", System.Windows.MessageBoxButton.OK);

        }
        #region AddAplication
        private ICommand _newApplicationCommand;
        public ICommand NewApplicationCommand =>
        _newApplicationCommand ??= new
        RelayCommand(OnNewApplicationExecuted);
        private void OnNewApplicationExecuted(object id)
        {
            if (Result1 < Weight)
            {
                System.Windows.MessageBox.Show($"Выберите другую дату!", "Внимание!", System.Windows.MessageBoxButton.OK);
                return;
            }

            var application = new Application
            {
                CargoName = CargoTitle,
                DateOfDispatch = SelectedDate,
                Destination = SelectedRoute.Destination,
                CargoWeight = Weight,
            };
            carManager.AddApplicationToCar(application,
            SelectedRoute.CarId);
            Applications.Add(application);
            System.Windows.MessageBox.Show($"Заявка зарегистрирована на {SelectedDate}!", "Внимание!", System.Windows.MessageBoxButton.OK);

        }
        #endregion



        //#region AddAplication
        //private ICommand _newApplication1Command;
        //public ICommand NewApplication1Command =>
        //_newApplicationCommand ??= new
        //RelayCommand(OnNewApplication1Executed);
        //private void OnNewApplication1Executed(object id)
        //{
        //    var dialog = new EditApplicationWindow
        //    {
        //        DateOfDispatch = DateTime.Now
        //    };
        //    if (dialog.ShowDialog() != true) return;
        //    var application = new Application
        //    {
        //        CargoName = dialog.CargoName,
        //        DateOfDispatch = dialog.DateOfDispatch,
        //        Destination = dialog.Destination,
        //        CargoWeight = dialog.CargoWeight,
        //    };
        //    carManager.AddApplicationToCar(application,
        //    _selectedCar.CarId);
        //    Applications.Add(application);
        //}
        //#endregion
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
        }
        #endregion
        #region TechnicalStatusWindow
        private ICommand _technicalStatusWindowCommand;
        public ICommand TechnicalStatusWindowCommand =>
        _technicalStatusWindowCommand ??= new
        RelayCommand(OnTechnicalStatusWindowExecuted);
        private void OnTechnicalStatusWindowExecuted(object id)
        {
            var dialog = new TechnicalStatusWindow
            {
            };
            if (dialog.ShowDialog() != true) return;
        }
        #endregion
        #region RouteWindow
        private ICommand _routeWindowCommand;
        public ICommand RouteWindowCommand =>
        _routeWindowCommand ??= new
        RelayCommand(OnRouteWindowExecuted);
        private void OnRouteWindowExecuted(object id)
        {
            var dialog = new RouteWindow
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
