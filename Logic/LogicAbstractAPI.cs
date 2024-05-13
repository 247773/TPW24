using Data;

namespace Logic
{
    public abstract class LogicAbstractAPI
    {
        public static LogicAbstractAPI CreateAPI(IDataTable dataAPI = null)
        {
            return new Table(dataAPI == null ? IDataTable.CreateAPI() : dataAPI);
        }

        public abstract void CreateBalls(int n, int r);
        public abstract void ClearTable();
        public abstract List<ILogicBall> GetBalls();
    }
}