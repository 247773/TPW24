using static Data.IDataBall;

namespace Data
{
    public class DataEventArgs
    {
        public BallPosition ballPosition;

        public DataEventArgs(BallPosition position)
        {
            ballPosition = position;
        }
    }
}
