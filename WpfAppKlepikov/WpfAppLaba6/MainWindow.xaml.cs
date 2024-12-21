using System.ComponentModel;
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
using System.Windows.Threading;

namespace WpfAppLaba6
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Intergral intergral;
        BackgroundWorker backgroundWorker;
        public MainWindow()
        {
            InitializeComponent();
            backgroundWorker = (BackgroundWorker)this.Resources["worker"];
        }

        private void buttonParams_Click(object sender, RoutedEventArgs e)
        {
            Window1 window1 = new Window1();
            if (window1.ShowDialog() != true) return;
            intergral = window1.intergral;
            //MessageBox.Show(intergral.ToString());
        }

        private void buttonD_Click(object sender, RoutedEventArgs e)
        {
            if (intergral == null) return;
            Thread thread = new Thread(Calculate);
            thread.Start();
        }

        private void buttonW_Click(object sender, RoutedEventArgs e)
        {
            buttonD.IsEnabled = false;
            buttonW.IsEnabled = false;
            backgroundWorker.RunWorkerAsync();
        }

        private async void buttonA_Click(object sender, RoutedEventArgs e)
        {
            listBox.Items.Clear();
            if (intergral == null) return;
            IAsyncEnumerable<(double,double,double)> data = intergral.GetDoublesAsync();
            await foreach (var trio in data)
            {
                listBox.Items.Add($"x = {trio.Item1:0.00} S = {trio.Item2:0.00000}");
                pBar.Value = trio.Item3 * 100;
            }

        }
        private void Calculate()
        {
            if (intergral == null) return;
            int n = intergral.N;
            double h = (intergral.B - intergral.A) / n;
            var step = Math.Round((double)(n) / 100);
            double S = 0;
            for (int i = 0; i <= n; i++)
            {
                double x = intergral.A + h * i;
                S += intergral.func(x) * h;
                if (i % step == 0)
                {
                    Dispatcher.BeginInvoke(DispatcherPriority.Normal,
                        new Action(() => pBar.Value = i / step));
                }
                Dispatcher.BeginInvoke
                    (
                        DispatcherPriority.Normal,
                        () => listBox.Items.Add(String.Format($"x = {x:0.00} S = {S:0.00000}"))
                    );
                Thread.Sleep(20);
            }
            MessageBox.Show($"S = {S:0.00000}");
        }

        private void BackgroundWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            if (intergral == null) return;
            int n = intergral.N;
            double h = (intergral.B - intergral.A) / n;
            var step = Math.Round((double)(n) / 100);
            double S = 0;
            for (int i = 0; i <= n; i++)
            {
                double x = intergral.A + h * i;
                S += intergral.func(x) * h;
                if (i % step == 0)
                {
                    if (backgroundWorker != null && backgroundWorker.WorkerReportsProgress)
                        backgroundWorker.ReportProgress((int)(i / step));
                }
                Dispatcher.BeginInvoke
                    (
                        DispatcherPriority.Normal,
                        () => listBox.Items.Add(String.Format($"x = {x:0.00} S = {S:0.00000}"))
                    );
                Thread.Sleep(20);
            }
            MessageBox.Show($"S = {S:0.00000}");

        }

        private void BackgroundWorker_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            pBar.Value = e.ProgressPercentage;
        }

        private void BackgroundWorker_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            buttonD.IsEnabled = true;
            buttonW.IsEnabled = true;
        }
    }
}