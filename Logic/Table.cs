using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    internal class Table
    {
        private int _length;
        private int _width;
        private List<Ball> _balls;


        public Table(int length, int width)
        {
            _length = length;
            _width = width;
            _balls = new List<Ball>();
        }

        public void createBalls(int numOfBalls, int radius)
        {
            for (int i=0; i < numOfBalls; i++)
            {
                Random rand = new Random();
                int x = rand.Next(radius, _length - radius);
                int y = rand.Next(radius, _width - radius);
                _balls.Add(new Ball(x, y, radius));
            }
        }

        public int Length
        {
            get { return _length; }
        }

        public int Width
        {
            get { return _width; }
        }

        public List<Ball> Balls
        {
            get { return _balls; }
        }
    }
}

