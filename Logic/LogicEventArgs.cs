namespace Logic
{
    public class LogicEventArgs
    {
        public ILogicBall Ball;
        public LogicEventArgs(ILogicBall ball)
        {
            Ball = ball;
        }
    }
}
