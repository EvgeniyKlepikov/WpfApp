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

namespace WpfAppLaba2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Driver driver = new Driver();
        public MainWindow()
        {
            InitializeComponent();
            datePickerDOB.SelectedDate = DateTime.Now;
            datePickerISS.SelectedDate = DateTime.Now;
            datePickerEXP.SelectedDate = DateTime.Now;
            //comboBox.Items.Add
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            driver.Name = textBoxName.Text;
            int temp;
            Int32.TryParse(textBoxNumber.Text, out temp);
            driver.Number = temp;
            driver.Address = textBoxAddress.Text;
            if(textBoxClass.Text.Length > 0)
            {
                driver.Class1 = textBoxClass.Text[0];
            }
            else
            {
                driver.Class1 = 'A';
            }
            driver.Dob = (DateTime)datePickerDOB.SelectedDate;
            driver.Iss = (DateTime)datePickerISS.SelectedDate;
            driver.Exp = (DateTime)datePickerEXP.SelectedDate;
            if(radioButtonMale.IsChecked == true) { driver.Gender = GENDER.male; }
            if (radioButtonFemale.IsChecked == true) { driver.Gender = GENDER.female; }
            if (radioButtonVariant.IsChecked == true) { driver.Gender = GENDER.variant; }
            driver.Donor = checkBoxDonor.IsChecked == true;
            driver.Hgt = slider.Value;

            MessageBox.Show(driver.ToString());
        }
    }
}