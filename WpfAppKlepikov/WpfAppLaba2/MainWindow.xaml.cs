using Microsoft.Win32;
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
            foreach(COLOREYES color in Enum.GetValues(typeof(COLOREYES)))
            {
                comboBox.Items.Add(color);
            }
            comboBox.SelectedIndex = 0;
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
            //driver.Eyes = comboBox.SelectedValuePath;
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

        private void ButtonLoad_Click(object sender, RoutedEventArgs e)
        {
            driver.Name = "Severus Snape";
            driver.Class1 = 'A';
            driver.Address = "Hogwarts";
            driver.Number = 0123456789;
            driver.Hgt = 192;
            driver.Gender = GENDER.variant;
            driver.Eyes = COLOREYES.gray;
            driver.Dob = new DateTime(1968, 5, 1);
            driver.Iss = new DateTime(2008, 10, 22);
            driver.Exp = new DateTime(2038, 10, 22);
            driver.Donor = true;
            driver.UriImage = "Images/man.jpg";

            textBoxName.Text = driver.Name;
            textBoxClass.Text = driver.Class1.ToString();
            textBoxAddress.Text = driver.Address;
            textBoxNumber.Text = driver.Number.ToString();
            slider.Value = driver.Hgt;
            if(driver.Gender == GENDER.male) radioButtonMale.IsChecked = true;
            if(driver.Gender == GENDER.female) radioButtonFemale.IsChecked = true;
            if(driver.Gender == GENDER.variant) radioButtonVariant.IsChecked = true;
            //comboBox.SelectedValue = driver.Eyes;
            comboBox.SelectedItem = driver.Eyes;
            datePickerDOB.SelectedDate = driver.Dob;
            datePickerISS.SelectedDate = driver.Iss;
            datePickerEXP.SelectedDate = driver.Exp;
            checkBoxDonor.IsChecked = driver.Donor;
            image.Source = new BitmapImage(new Uri(driver.UriImage, UriKind.RelativeOrAbsolute));
            //MessageBox.Show(driver.ToString());
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Файлы (jpg)|*.jpg|Все файлы|*.*";
            if (dialog.ShowDialog() == true)
            {
                driver.UriImage = dialog.FileName;
                image.Source = new BitmapImage(new Uri(dialog.FileName, UriKind.RelativeOrAbsolute));
            }
        }

        private void ButtonClear_Click(object sender, RoutedEventArgs e)
        {
            textBoxName.Text = null;
            textBoxClass.Text = null;
            textBoxAddress.Text = null;
            textBoxNumber.Text = null;
            slider.Value = driver.Hgt;
            if (driver.Gender == GENDER.male) radioButtonMale.IsChecked = true;
            if (driver.Gender == GENDER.female) radioButtonFemale.IsChecked = true;
            if (driver.Gender == GENDER.variant) radioButtonVariant.IsChecked = true;
            comboBox.SelectedIndex = 0;
            datePickerDOB.SelectedDate = DateTime.Now;
            datePickerISS.SelectedDate = DateTime.Now;
            datePickerEXP.SelectedDate = DateTime.Now;
            checkBoxDonor.IsChecked = driver.Donor;
            image.Source = new BitmapImage(new Uri(driver.UriImage, UriKind.RelativeOrAbsolute));

        }
    }
}