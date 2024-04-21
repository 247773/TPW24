using Data;

namespace Logic
{
    public abstract class LogicAbstractAPI
    {
        public static LogicAbstractAPI CreateAPI(DataAbstractAPI data = null)
        {
            return new Table(580, 420, data ?? DataAbstractAPI.CreateAPI());
        }

        public abstract void CreateBalls(int numOfBalls, int r);
        public abstract void StartSimulation();
        public abstract void ClearTable();
        public abstract List<List<int>> GetBallsPosition();
        public abstract List<IBall> GetBalls();
    }
}
