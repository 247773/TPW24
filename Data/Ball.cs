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

        public void Move(int dx, int dy)
        {
            _x += dx;
            _y += dy;
        }
    }
}
