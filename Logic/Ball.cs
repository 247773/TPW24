using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Logic
{
    internal class Ball : IBall, INotifyPropertyChanged
    {


        public override event PropertyChangedEventHandler? PropertyChanged;
        public override int X
        {
            get => _X;
            set { _X = value; RaisePropertyChanged(); }
        }
        public override int Y
        {
            get => _Y;
            set { _Y = value; RaisePropertyChanged(); }
        }

        public override int R
        {
            get => _R;
            set { _R = value; }
        }

        public int _X { get; set; }
        public int _Y { get; set; }
        public int _R { get; set; }
        public int _Vx { get; set; }
        public int _Vy { get; set; }

        internal Ball(int x, int y, int r)
        {
            this._X = x;
            this._Y = y;
            this._R = r;
        }

        public override void MoveBall()
        {
            X += _Vx;
            Y += _Vy;
        }

        public override bool IsWithinBounds(int length, int width)
        {
            bool isWithinXBounds = this._X + this._Vx + this._R < length && this._X + this._Vx - this._R > 0;
            bool isWithinYBounds = this._Y + this._Vy + this._R < width && this._Y + this._Vy - this._R > 0;

            return isWithinXBounds && isWithinYBounds;
        }

        public override void RandomVelocity(int Vmin, int Vmax)
        {
            Random rand = new Random();
            this._Vy = rand.Next(Vmin, Vmax);
            this._Vx = rand.Next(Vmin, Vmax);
        }

        private void RaisePropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

