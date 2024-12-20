using System.IO;
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
using System.Xml.Serialization;
using Microsoft.Win32;

namespace WpfAppLaba5
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Shape shape;
        public MainWindow()
        {
            InitializeComponent();
            CommandBinding bindingHelp = new CommandBinding();
            bindingHelp.Command = ApplicationCommands.Help;
            bindingHelp.Executed += Help;
            menuitemHelp.CommandBindings.Add(bindingHelp);
            buttonHelp.CommandBindings.Add(bindingHelp);

            CommandBinding bindingOpen = new CommandBinding();
            bindingOpen.Command = ApplicationCommands.Open;
            bindingOpen.Executed += Load;
            menuLoad.CommandBindings.Add(bindingOpen);

            CommandBinding bindingSave = new CommandBinding();
            bindingSave.Command = ApplicationCommands.Save;
            bindingSave.Executed += Save;
            menuSave.CommandBindings.Add(bindingSave);

        }

        private void Save(object sender, ExecutedRoutedEventArgs e)
        {
            if (shape == null) return;
            shape.Save();
        }

        private void Load(object sender, ExecutedRoutedEventArgs e)
        {
            shape = Shape.Load();
        }

        private void Help(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("Справка по приложению");
        }

        private void MenuItemShape_Click(object sender, RoutedEventArgs e)
        {
            WindowShape windowShape = new WindowShape();
            if(windowShape.ShowDialog() == false) return;
            shape = windowShape.GetShape();
            //MessageBox.Show(shape.ToString());
        }

        private void canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(shape == null) return;
            shape.Draw(canvas, e.GetPosition(canvas));
        }

        private void canvas_MouseMove(object sender, MouseEventArgs e)
        {
            textBlock.Text = $"x = {e.GetPosition(canvas).X:0.00} y = {e.GetPosition(canvas).Y:0.00}";
        }
    }
}