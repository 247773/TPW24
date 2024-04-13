using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public abstract class LogicAbstractAPI
    {
        public static LogicAbstractAPI CreateLogicAPI()
        {
            return new Table(580, 420);
        }

        public abstract void CreateBalls(int numOfBalls, int r);
        public abstract void StartSimulation();
        public abstract void ClearTable();
        public abstract List<List<int>> GetBallsPosition();
        public abstract List<IBall> GetBalls();
    }
}
