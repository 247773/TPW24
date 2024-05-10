using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Data
{
    internal class DataBall : IDataBall
    {
        private int _x;
        private int _y;

        public override event PropertyChangedEventHandler? PropertyChanged;

        public override int X
        {
            get => _x;
            set { _x = value; RaisePropertyChanged(); }
        }

        public override int Y
        {
            get => _y;
            set { _y = value; RaisePropertyChanged(); }
        }

        public override int R { get; set; }
        public override int M { get; set; }
        public override int Vx { get; set; }
        public override int Vy { get; set; }

        internal DataBall(int x, int y, int r, int m, int vX, int vY)
        {
            this._x = x;
            this._y = y;
            this.R = r;
            this.M = m;
            this.Vx = vX;
            this.Vy = vY;
            Task task = Task.Run(StartSimulation);
        }

        public void MoveBall()
        {
            X += Vx;
            Y += Vy;
        }

        public void StartSimulation()
        {
            while (true)
            {
                MoveBall();
                Task.Delay(10).Wait();
            }
        }

        private void RaisePropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
