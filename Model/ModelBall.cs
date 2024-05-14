using Logic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Model
{
    internal class ModelBall : IModelBall, INotifyPropertyChanged
    {
        private int _x;
        private int _y;
        private int _r;

        public override int PositionX { get => _x; }
        public override int PositionY { get => _y; }
        public override int Radius { get => _r; }

        // TODO: skalowanie

        public ModelBall(int x, int y, int r)
        {
            _x = x;
            _y = y;
            _r = r;
        }

        public override event PropertyChangedEventHandler? PropertyChanged;

        public override void UpdateModelBall(Object s, LogicEventArgs e)
        {
            ILogicBall ball = (ILogicBall)s;
            _x = (int) ball.Position.X;
            RaisePropertyChanged("PositionX");
            _y = (int) ball.Position.Y;
            RaisePropertyChanged("PositionY");
        }

        // TODO: zamienić string na liczby -> eventArgs
        private void RaisePropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}