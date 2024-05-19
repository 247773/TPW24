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

        // Stała reprezentująca współczynnik skalowania
        private const double _scaleFactor = 1.0;

        public override int PositionX { get => (int)(_x * _scaleFactor); }
        public override int PositionY { get => (int)(_y * _scaleFactor); }
        public override int Radius { get => (int)(_r * _scaleFactor); }

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

        private void RaisePropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}