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
            return new LogicAPI();
        }

        public abstract void CreateTable(int length, int width, int numOfBalls, int radius);
        public abstract List<Ball> GetBalls();
    }
}
