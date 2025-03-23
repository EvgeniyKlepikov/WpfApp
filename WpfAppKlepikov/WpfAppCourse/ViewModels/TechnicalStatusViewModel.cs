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
    public class TechnicalStatusViewModel : ViewModelBase
    {
        ManagersFactory factory;
        CarManager carManager;
        TechnicalStatusManager technicalStatusManager;
        private string title = "Технический осмотр";
        public ObservableCollection<Car> Cars { get; set; }
        public ObservableCollection<TechnicalStatus> TechnicalStatuses { get; set; }
        public string Title { get => title; set => title = value; }

        public TechnicalStatusViewModel()
        {
            factory = new ManagersFactory("DefaultConnection");
            carManager = factory.GetCarManager();
            technicalStatusManager = factory.GetTechnicalStatusManager();
            Cars = new ObservableCollection<Car>(carManager.Cars);
            TechnicalStatuses = new ObservableCollection<TechnicalStatus>();
            if (Cars.Count() > 0)
                OnGetTechnicalStatusExecuted(Cars[0].CarId);
        }
        private Car _selectedCar;

        public Car SelectedCar
        {
            get => _selectedCar;
            set
            {
                Set(ref _selectedCar, value);
            }
        }
        #region Commands
        private ICommand _getTechnicalStatusesCommand;
        public ICommand GetTechnicalStatusesCommand
            => _getTechnicalStatusesCommand
            ??= new RelayCommand(OnGetTechnicalStatusExecuted);
        private void OnGetTechnicalStatusExecuted(object id)
        {
            TechnicalStatuses.Clear();
            var technicalStatuses = carManager.GetTechnicalStatusesOfCar((int)id);
            foreach (var technicalStatus in technicalStatuses)
                TechnicalStatuses.Add(technicalStatus);
        }
        #endregion Commands
        #region TechnicalStatus
        private ICommand _newTechnicalStatusCommand;
        public ICommand NewTechnicalStatusCommand =>
        _newTechnicalStatusCommand ??= new
        RelayCommand(OnNewTechnicalStatusExecuted);
        private void OnNewTechnicalStatusExecuted(object id)
        {
            var dialog = new EditTechnicalStatusWindow
            {
                DateOfStatus = DateTime.Now
            };
            if (dialog.ShowDialog() != true) return;
            var technicalStatus = new TechnicalStatus
            {
                DateOfStatus = dialog.DateOfStatus,
            };
            carManager.AddTechnicalStatusToCar(technicalStatus, _selectedCar.CarId);
            TechnicalStatuses.Add(technicalStatus);
        }
        #endregion
        #region Выбранный водитель
        private TechnicalStatus _selectedTechnicalStatus;
        public TechnicalStatus SelectedTechnicalStatus
        {
            get => _selectedTechnicalStatus;
            set
            {
                Set(ref _selectedTechnicalStatus, value);
            }
        }
        #endregion
        #region Редактирование водителя
        private ICommand _editTechnicalStatusCommand;
        public ICommand EditTechnicalStatusCommand =>
        _editTechnicalStatusCommand ??=
        new RelayCommand(OnEditTechnicalStatusExecuted,
        EditTechnicalStatusCanExecute);
        // Проверка возможности редактирования
        private bool EditTechnicalStatusCanExecute(object p) =>

        _selectedTechnicalStatus != null;

        private void OnEditTechnicalStatusExecuted(object id)
        {
            var dialog = new EditTechnicalStatusWindow
            {
                DateOfStatus = _selectedTechnicalStatus.DateOfStatus,
            };
            if (dialog.ShowDialog() != true) return;
            // Сохранение параметров
            _selectedTechnicalStatus.DateOfStatus = dialog.DateOfStatus;
            technicalStatusManager.UpdateTechnicalStatus(_selectedTechnicalStatus);
            // Обновить список заявок
            OnGetTechnicalStatusExecuted(_selectedCar.CarId);
        }
        #endregion
        public ICommand _deleteTechnicalStatusCommand;

        public ICommand DeleteTechnicalStatusCommand
            => _deleteTechnicalStatusCommand
            ??= new RelayCommand(OnDeleteTechnicalStatusExecuted, DeleteTechnicalStatusCanExecute);
        // Проверка возможности удаления
        private bool DeleteTechnicalStatusCanExecute(object p) =>

        _selectedTechnicalStatus != null;


        private void OnDeleteTechnicalStatusExecuted(object ob)
        {
            if (SelectedTechnicalStatus != null)
            {
                var result = System.Windows.MessageBox.Show($"Осмотр за {SelectedTechnicalStatus.DateOfStatus} будет удален!", "Подтверждение", System.Windows.MessageBoxButton.YesNo);
                if (result == System.Windows.MessageBoxResult.Yes)
                {
                    technicalStatusManager.DeleteTechnicalStatus(_selectedTechnicalStatus.TechnicalStatusId);
                    //applicationManager.UpdateApplication(_selectedApplication);
                    OnGetTechnicalStatusExecuted(_selectedTechnicalStatus.CarId);
                }
            }
        }

    }
}
