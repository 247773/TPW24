using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Data;

namespace Logic
{
    public abstract class LogicAbstractAPI
    {
        public static LogicAbstractAPI CreateLogicAPI()
        {
            return new Table(259, 146); // wymiary stolu bilarodwego typu 8ft
        }

        public abstract void CreateBalls(int numOfBalls, int radius); //operacja interaktywna (twórz kule)
        public abstract void UpdateBalls();
        public abstract List<(int, int)> GetBallsPosition(); //operacja reaktywna (okresowe wysłanie położenia kul)
    }
}
