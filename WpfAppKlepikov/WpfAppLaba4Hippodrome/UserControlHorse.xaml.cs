using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfAppLaba4Hippodrome
{
    /// <summary>
    /// Логика взаимодействия для UserControlHorse.xaml
    /// </summary>
    public partial class UserControlHorse : UserControl
    {
        Horse horse;
        public UserControlHorse(int speed)
        {
            InitializeComponent();
            horse = new Horse(speed);
            this.DataContext = horse;

            Binding bindingSpeed = new Binding("Speed");
            textBlockSpeed.SetBinding(TextBlock.TextProperty, bindingSpeed);

            Binding bindingPosition = new Binding("Position");
            textBlockPosition.SetBinding(TextBlock.TextProperty, bindingPosition);

            this.SetBinding(Canvas.LeftProperty, new Binding("X"));
        }
        public int GetSpeed()
        {
            return horse.Speed;
        }
        public void SetSpeed(int speed)
        {
            horse.Speed = speed;
        }
        public void SetPosition(int position) 
        {
            horse.Position = position;
        }
        public float X 
        { 
            get
            {
                return horse.X;
            } 
            set
            {
                horse.X = value;
            } 

        }
        public bool IsFinish
        {
            get
            {
                return horse.IsFinish;
            }
            set
            {
                horse.IsFinish = value;
            }
        }

    }
}
