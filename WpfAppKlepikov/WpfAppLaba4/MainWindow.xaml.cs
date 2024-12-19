﻿using System.Text;
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

namespace WpfAppLaba4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Canvas[] canvases = new Canvas[3];
        WpfAppLaba4Hippodrome.UserControlHorse[] horses = new WpfAppLaba4Hippodrome.UserControlHorse[3];
        WpfAppLaba4Hippodrome.UserControlFinish[] finishes = new WpfAppLaba4Hippodrome.UserControlFinish[3];
        WpfAppLaba4Hippodrome.UserControlPosition[] positions = new WpfAppLaba4Hippodrome.UserControlPosition[3];
        DispatcherTimer timer, timerUpdateSpeed;
        Random Random = new Random();
        bool flStart = false;
        public MainWindow()
        {
            InitializeComponent();
            this.ResizeMode = ResizeMode.NoResize;
            for (int i = 0; i < canvases.Length; i++)
            {
                canvases[i] = new Canvas();
                Grid.SetRow(canvases[i], i);
                grid.Children.Add(canvases[i]);                
            }
            timer = new DispatcherTimer();
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = new TimeSpan(1000);
            timerUpdateSpeed = new DispatcherTimer();
            timerUpdateSpeed.Tick += new EventHandler(timerUpdateSpeed_Tick);
            timerUpdateSpeed.Interval = new TimeSpan(0,0,2);



        }
        private void Start()
        {
            for (int i = 0; i < canvases.Length; i++)
            {
                canvases[i].Children.Clear();
                finishes[i] = new WpfAppLaba4Hippodrome.UserControlFinish();
                positions[i] = new WpfAppLaba4Hippodrome.UserControlPosition();
                canvases[i].Children.Add(positions[i]);
                Canvas.SetLeft(positions[i], 50);
                canvases[i].Children.Add(finishes[i]);
                Canvas.SetRight(finishes[i], 0);

                horses[i] = new WpfAppLaba4Hippodrome.UserControlHorse(Random.Next(20, 50));
                canvases[i].Children.Add(horses[i]);
            }
            timer.Start();
            timerUpdateSpeed.Start();
        }


        private void timerUpdateSpeed_Tick(object? sender, EventArgs e)
        {
            for (int i = 0; i < canvases.Length; i++)
            {
                if (horses[i].IsFinish == false)
                    horses[i].SetSpeed(Random.Next(30, 80));
            }
        }

        private void ButtonStart_Click(object sender, RoutedEventArgs e)
        {
            if(flStart == false)
            {
                flStart = true;
                Start();
            }
            if (timer.IsEnabled)
            {
                ((Button)sender).Content = "Start";
                timer.Stop();
                timerUpdateSpeed.Stop();
            }
            else
            {
                ((Button)sender).Content = "Pause";
                timer.IsEnabled = true;
                timerUpdateSpeed.IsEnabled = true;
            }
        }

        private void ButtonReset_Click(object sender, RoutedEventArgs e)
        {
            Start();
        }

        private void timer_Tick(object? sender, EventArgs e)
        {
            for (int i = 0; i < canvases.Length; i++)
            {
                if (horses[i].IsFinish)
                    continue;
                int k = 0; // Позиция в забеге
                for (int j = 0; j < canvases.Length; j++)
                {
                    if (horses[j].IsFinish || horses[i].X <= horses[j].X) k++;
                }
                if (horses[i].X < 1100) // еще не финишировала
                {
                    horses[i].SetPosition(k);
                    horses[i].X += horses[i].GetSpeed() / 1500.0f;
                }
                else // лошадка на финише в первый раз
                {
                    horses[i].SetPosition(k);
                    positions[i].SetPosition(k);
                    horses[i].IsFinish = true;
                }
            }
        }
    }
}