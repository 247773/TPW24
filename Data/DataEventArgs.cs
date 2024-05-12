namespace Data
{
    public class DataEventArgs
    {
        public IDataBall Ball;
        public DataEventArgs(IDataBall ball)
        {
            Ball = ball;
        }
    }
}
