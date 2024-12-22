using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfAppLaba7
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Person person = new Person();
        public Company company = new Company();
        ObservableCollection<Person> people = new ObservableCollection<Person>();
        ObservableCollection<Company> companies = new ObservableCollection<Company>();
        public MainWindow()
        {
            InitializeComponent();
            LoadData();
        }
        private void LoadData()
        {
            int indexCompany = CompanyListView.SelectedIndex;
            int indexPerson = PersonListView.SelectedIndex;

            // Company
            companies = Company.GetAll();
            CompanyListView.SelectionChanged -= CompanyListView_SelectionChanged;
            CompanyListView.ItemsSource = companies;
            if (indexCompany < 0) indexCompany = 0;
            CompanyListView.SelectedIndex = indexCompany;
            CompanyListView.SelectionChanged += CompanyListView_SelectionChanged;
            CompanyComboBox.ItemsSource = companies;
            CompanyStackPanel.DataContext = company;

            // Person
            if (indexPerson < 0) indexPerson = 0;
            people = Person.GetByCompanyId(((Company)CompanyListView.SelectedItem).Id);
            PersonListView.ItemsSource = people;
            PersonListView.SelectedIndex = indexPerson;
            PersonStackPanel.DataContext = person;

        }
        private void AddCompanyButton_Click(object sender, RoutedEventArgs e)
        {
            company.Insert();
            LoadData();
            CompanyListView.SelectedIndex = CompanyListView.Items.Count - 1;
        }

        private void UpdateCompanyButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedCompany = (Company)CompanyListView.SelectedItem;
            company.Id = selectedCompany.Id;
            if (CompanyNameTextBox.Text == string.Empty)
                company.Name = selectedCompany.Name;
            if (HeadquartersTextBox.Text == string.Empty)
                company.Headquarters = selectedCompany.Headquarters;
            if (CompanyEstablishmentDatePicker.SelectedDate.HasValue
                && CompanyEstablishmentDatePicker.SelectedDate > DateTime.MinValue)
                company.DateEstablishment = (DateTime)CompanyEstablishmentDatePicker.SelectedDate;
            else
                company.DateEstablishment = selectedCompany.DateEstablishment;
            company.Update();
            LoadData();
        }

        private void DeleteCompanyButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedCompany = (Company)CompanyListView.SelectedItem;
            if (selectedCompany != null)
            {
                selectedCompany.Delete();
                LoadData();
            }
        }

        private void ClearCompanyButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void FindCompanyButton_Click(object sender, RoutedEventArgs e)
        {
            string searchName = CompanyNameTextBox.Text;
            string searchHeadquarters = HeadquartersTextBox.Text;
            DateTime? searchDateEstablishment = CompanyEstablishmentDatePicker.SelectedDate;
            var query = from c in Company.GetAll()
                        where (string.IsNullOrEmpty(searchName) || c.Name.Contains(searchName))
                        && (string.IsNullOrEmpty(searchHeadquarters) || c.Headquarters.Contains(searchHeadquarters))
                        && (!searchDateEstablishment.HasValue || searchDateEstablishment.Value == DateTime.MinValue || c.DateEstablishment.Date == searchDateEstablishment.Value.Date)
                        select c;
            CompanyListView.SelectionChanged -= CompanyListView_SelectionChanged;
            CompanyListView.ItemsSource = query.ToList();
            CompanyListView.SelectionChanged += CompanyListView_SelectionChanged;
        }

        private void CompanyListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = companies.ToList().FindIndex(c => c.Id == ((Company)CompanyListView.SelectedItem).Id);
            people = Person.GetByCompanyId(((Company)CompanyListView.SelectedItem).Id);
            PersonListView.ItemsSource = people;
        }

        private void AddPersonButton_Click(object sender, RoutedEventArgs e)
        {
            person.Insert();
            LoadData();
            PersonListView.SelectedIndex = PersonListView.Items.Count - 1;
        }

        private void UpdatePersonButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedPerson = (Person)PersonListView.SelectedItem;
            if (PersonNameTextBox.Text == string.Empty)
                person.Name = selectedPerson.Name;
            if (PersonDateBirthDatePicker.SelectedDate == null)
                person.DateBirth = selectedPerson.DateBirth;
            if (PersonSalaryTextBox.Text == string.Empty)
                person.Salary = selectedPerson.Salary;

            person.Update();
            LoadData();
        }

        private void DeletePersonButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedPerson = (Person)PersonListView.SelectedItem;
            if (selectedPerson != null)
            {
                selectedPerson.Delete();
                LoadData();
            }
        }

        private void ClearPersonButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CompanyComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void PersonListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}