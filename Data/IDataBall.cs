using System.Numerics;

namespace Data
{
    public abstract class IDataBall
    {
        public abstract Vector2 Position { get; }
        public abstract Vector2 Velocity { get; set; }
        public abstract void Dispose();

        public abstract event EventHandler<DataEventArgs> ChangedPosition;

        public static IDataBall CreateDataBall(float x, float y, int r, int m, float vX, float vY)
        {
            return new DataBall(x, y, r, m, vX, vY);
        }
    }
}