using System.Numerics;
using static Data.IDataBall;

namespace Data
{
    internal record LogBall
    {
        public int ID;
        public BallPosition Position;
        public BallVelocity Velocity;
        public long Time;
        public LogBall(BallPosition p, BallVelocity v, long t, int id)
        {
            Position = p;
            Velocity = v;
            Time = t;
            ID = id;
        }
    }
}
