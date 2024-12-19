using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppLaba4Hippodrome
{
    class Horse
    {
        int speed = 0;
        int position = 0;
        float x = 0;
        bool isFinish = false;

        public Horse(int speed)
        {
            this.Speed = speed;
        }

        public int Speed { get => speed; set => speed = value; }
        public int Position { get => position; set => position = value; }
        public float X { get => x; set => x = value; }
        public bool IsFinish { get => isFinish; set => isFinish = value; }
    }
}
