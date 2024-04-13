using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public abstract class IBall
    {
        public static IBall CreateBall(int x, int y, int r)
        {
            return new Ball(x, y, r);
        }

        public abstract int X { get; set; }
        public abstract int Y { get; set; }
        public abstract int R { get; set; }
        public abstract void RandomVelocity(int min, int max);
        public abstract void MoveBall();
        public abstract bool CheckCollision(int BoardWidth, int BoardHeight);

        public abstract event PropertyChangedEventHandler? PropertyChanged;
    }
}
