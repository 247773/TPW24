using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class Ball
    {
        private int _x;
        private int _y;
        private int _r;
        private int _vx;
        private int _vy;

        public Ball(int x, int y, int r)
        {
            this._x = x;
            this._y = y;
            this._r = r;
        }

        public int X
        {
            get { return _x; }
            set { _x = value; }
        }

        public int Y
        {
            get { return _y; }
            set { _y = value; }
        }

        public int R
        {
            get { return _r; }
        }

        public int Vx
        {
            get { return _vx; }
            set { _vx = value; }
        }

        public int Vy
        {
            get { return _vy; }
            set { _vy = value; }
        }

        public void Move()
        {
            this._x += _vx;
            this._y += _vy;
        }

        public void RandomVelocity(int vMin, int vMax)
        {
            Random rand = new Random();
            this._vx = rand.Next(vMin, vMin);
            this._vy = rand.Next(vMin, vMax);
        }

        public bool checkIfOutOfBounds(int length, int width)
        {
            if (this._x - this._r < 0 || this._x + this._r > length || this._y - this._r < 0 || this._y + this._r > width)
            {
                return true;
            }
            return false;
        }
    }
}
