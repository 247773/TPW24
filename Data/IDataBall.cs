using System.ComponentModel;

namespace Data
{
    public abstract class IDataBall
    {
        public static IDataBall CreateDataBall(int x, int y, int r, int m, int vX, int vY)
        {
            return new DataBall(x, y, r, m, vX, vY);
        }

        public abstract int X { get; set; }
        public abstract int Y { get; set; }
        public abstract int R { get; set; }
        public abstract int M { get; set; } // Masa kuli
        public abstract int Vx { get; set; }
        public abstract int Vy { get; set; }

        public abstract event PropertyChangedEventHandler? PropertyChanged;
    }
}
