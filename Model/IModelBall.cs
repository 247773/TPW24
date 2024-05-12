using Logic;
using System.ComponentModel;

namespace Model
{
    public abstract class IModelBall
    {
        public static IModelBall CreateModelBall(int x, int y, int r)
        {
            return new ModelBall(x, y, r);
        }

        public abstract int PositionX { get; }
        public abstract int PositionY { get; }
        public abstract int Radius { get; }

        public abstract void UpdateModelBall(Object o, LogicEventArgs e);

        public abstract event PropertyChangedEventHandler? PropertyChanged;
    }
}
