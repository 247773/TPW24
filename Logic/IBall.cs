using System.ComponentModel;

namespace Logic
{
    public abstract class IBall
    {
        public static IBall CreateBall(int x, int y, int r)
        {
            return new Ball(x, y, r);
        }

        public abstract double X { get; set; }
        public abstract double Y { get; set; }
        public abstract int R { get; set; }
        public abstract double Vx { get; set; }
        public abstract double Vy { get; set; }
        public abstract bool BouncedBack { get; set;}
        
        public abstract void MoveBall();
        public abstract void CheckCollision(int length, int width);
        public abstract void CheckBallCollision(IBall ball);
        public abstract void UseTempSpeed();

        public abstract event PropertyChangedEventHandler? PropertyChanged;
    }
}
