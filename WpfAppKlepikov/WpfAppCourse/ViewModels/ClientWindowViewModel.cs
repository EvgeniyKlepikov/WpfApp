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
    public class ClientWindowViewModel : ViewModelBase
    {
        ManagersFactory factory;
        ClientManager clientManager;
        private string title = "Заказчики";
        public ObservableCollection<Client> Clients { get; set; }
        public string Title { get => title; set => title = value; }
        //public int deposit;

        private double _deposit;
        public double Deposit
        {
            get => _deposit;
            set
            {
                Set(ref _deposit, value);
            }
        }


        public ClientWindowViewModel()
        {
            factory = new ManagersFactory("DefaultConnection");
            clientManager = factory.GetClientManager();
            Clients = new ObservableCollection<Client>(clientManager.GetAllClients());
            //if (Clients.Count() > 0)
            //    OnGetClientExecuted(Clients[0].ClientId);
            //if (Cars.Count() > 0)
            //    OnGetApplicationExecuted(Cars[0].CarId);

            Deposit = 0.0;
        }

        #region Commands
        private ICommand _getClientsCommand;
        public ICommand GetClientsCommand
            => _getClientsCommand
            ??= new RelayCommand(OnGetClientExecuted);
        private void OnGetClientExecuted(object id)
        {
            Clients.Clear();
            var clients = clientManager.GetAllClients();
            foreach (var client in clients)
                Clients.Add(client);
            //_selectedClient = Clients[(int)id];
        }
        #endregion Commands
        #region Создание нового заказчика
        private ICommand _newClientCommand;
        public ICommand NewClientCommand =>
        _newClientCommand ??= new
        RelayCommand(OnNewClientExecuted);
        private void OnNewClientExecuted(object id)
        {
            var dialog = new EditClientWindow
            {
            };
            if (dialog.ShowDialog() != true) return;
            var client = new Client
            {
                ClientName = dialog.ClientName,
                ClientSurname = dialog.ClientSurname,
                Company = dialog.Company,
                ClientDeposit = 0.0,
            };
            clientManager.CreateClient(client);
            Clients.Add(client);
        }
        #endregion
        #region Выбранный заказчик
        private Client _selectedClient;
        public Client SelectedClient
        {
            get => _selectedClient;
            set
            {
                Set(ref _selectedClient, value);
            }
        }
        #endregion
        #region Редактирование заказчика
        private ICommand _editClientCommand;
        public ICommand EditClientCommand =>
        _editClientCommand ??=
        new RelayCommand(OnEditClientExecuted, EditClientCanExecute);
        // Проверка возможности редактирования
        private bool EditClientCanExecute(object p) =>

        _selectedClient != null;

        private void OnEditClientExecuted(object id)
        {
            var dialog = new EditClientWindow
            {
                ClientName = _selectedClient.ClientName,
                ClientSurname = _selectedClient.ClientSurname,
                Company = _selectedClient.Company,
            };
            if (dialog.ShowDialog() != true) return;
            // Сохранение параметров
            _selectedClient.ClientName = dialog.ClientName;
            _selectedClient.ClientSurname = dialog.ClientSurname;
            _selectedClient.Company = dialog.Company;
            clientManager.UpdateClient(_selectedClient);
            // Обновить список заявок
            OnGetClientExecuted(_selectedClient.ClientId);
        }
        #endregion
        public ICommand _deleteClientCommand;

        public ICommand DeleteClientCommand
            => _deleteClientCommand
            ??= new RelayCommand(OnDeleteClientExecuted, DeleteClientCanExecute);
        // Проверка возможности удаления
        private bool DeleteClientCanExecute(object p) =>

        _selectedClient != null;


        private void OnDeleteClientExecuted(object ob)
        {
            if (SelectedClient != null)
            {
                var result = System.Windows.MessageBox.Show($"Клиент {SelectedClient.ClientName} будет удален!", "Подтверждение", System.Windows.MessageBoxButton.YesNo);
                if (result == System.Windows.MessageBoxResult.Yes)
                {
                    clientManager.DeleteClient(_selectedClient.ClientId);
                    //clientManager.UpdateClient(_selectedClient);
                    OnGetClientExecuted(_selectedClient.ClientId);
                }
            }
        }
        #region Пополнение баланса
        private ICommand _addDepositCommand;
        public ICommand AddDepositCommand =>
        _addDepositCommand ??= new
        RelayCommand(OnAddDepositExecuted, AddDepositCanExecute);
        // Проверка возможности редактирования
        private bool AddDepositCanExecute(object p) =>

        _selectedClient != null;

        private void OnAddDepositExecuted(object id)
        {
            //var dialog = new EditClientWindow
            //{
            //};
            //if (dialog.ShowDialog() != true) return;
            //var client = new Client
            //{
            //    ClientName = dialog.ClientName,
            //    ClientSurname = dialog.ClientSurname,
            //    Company = dialog.Company,
            //    ClientDeposit = 0,
            //};
            _selectedClient.ClientDeposit += Deposit;

            clientManager.UpdateClient(_selectedClient);
            // Обновить список заявок
            OnGetClientExecuted(_selectedClient.ClientId);
            //SelectedClient = Clients[_selectedClient.ClientId];


        }
        #endregion

    }
}
