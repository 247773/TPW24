using System.Numerics;
using static Data.IDataBall;

namespace Data
{
    internal record LogBall
    {
        public int ID;
        public BallPosition Position;
        public BallVelocity Velocity;
        public DateTime Time;
        public LogBall(BallPosition p, BallVelocity v, DateTime t, int id)
        {
            Position = p;
            Velocity = v;
            Time = t;
            ID = id;
        }
    }
}
