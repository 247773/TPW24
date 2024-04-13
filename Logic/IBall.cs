using System.ComponentModel;

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
        public abstract void RandomVelocity(int Vmin, int Vmax);
        public abstract void MoveBall();
        public abstract bool IsWithinBounds(int length, int width);

        public abstract event PropertyChangedEventHandler? PropertyChanged;
    }
}
