using Logic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Logic
{
    public class Ball : IBall
    {
        public override event PropertyChangedEventHandler? _propertyChanged;

        public Ball(int x, int y, int r)
        {
            this._x = x;
            this._y = y;
            this._r = r;
        }

        public override int _x
        {
            get => _x;
            set 
            { 
                _x = value;
                RaisePropertyChanged();
            }
        }

        public override int _y
        {
            get => _y;
            set 
            { 
                _y = value;
                RaisePropertyChanged();
            }
        }

        public override int _r
        {
            get { return _r; }
            set
            {
                _r = value;
            }
        }

        public override int _vx
        {
            get { return _vx; }
            set { _vx = value; }
        }

        public override int _vy
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

        private void RaisePropertyChanged([CallerMemberName] string? propertyName = null)
        {
            _propertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
