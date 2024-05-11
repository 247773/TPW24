using System.ComponentModel;

namespace Data
{
    public abstract class IDataBall
    {
        public static IDataBall CreateDataBall(int x, int y, int r, int m, int vX, int vY)
        {
            return new DataBall(x, y, r, m, vX, vY);
        }

        public abstract double X { get; set; }
        public abstract double Y { get; set; }
        public abstract int R { get; set; }
        public abstract int M { get; set; }
        public abstract double Vx { get; set; }
        public abstract double Vy { get; set; }
        public abstract double TempVx { get; set; }
        public abstract double TempVy { get; set; }
        public abstract bool IsMoved { get; set; }
        public abstract void MoveBall();

        public abstract event PropertyChangedEventHandler? PropertyChanged;
    }
}
