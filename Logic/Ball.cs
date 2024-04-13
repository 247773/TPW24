using Logic;
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
            set { _R = value; RaisePropertyChanged(); }
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

        public override bool CheckCollision(int BoardWidth, int BoardHeight)
        {
            if (this._X + this._Vx + this._R < BoardWidth && this._X + this._Vx - this._R > 0
                && this._Y + this._Vy + this._R < BoardHeight && this._Y + this._Vy - this._R > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override void RandomVelocity(int min, int max)
        {
            Random rand = new Random();
            this._Vy = rand.Next(min, max);
            this._Vx = rand.Next(min, max);
        }

        private void RaisePropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

