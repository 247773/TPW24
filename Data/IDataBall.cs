using System.Numerics;

namespace Data
{
    public abstract class IDataBall
    {
        public record BallPosition(float X, float Y);
        public record BallVelocity(float X, float Y);
        public abstract BallPosition Position { get; }
        public abstract BallVelocity Velocity { get; set; }

        // public abstract Vector2 Position { get; }
        // public abstract Vector2 Velocity { get; set; }
        public abstract int ID { get; }
        public abstract void Dispose();

        public abstract event EventHandler<DataEventArgs> ChangedPosition;
    }
}