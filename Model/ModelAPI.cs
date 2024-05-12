using Logic;
using System.Collections.ObjectModel;

namespace Model
{
    internal class ModelAPI : ModelAbstractAPI
    {
        private LogicAbstractAPI _logicAPI;
        private ObservableCollection<IModelBall> _modelBalls = new ObservableCollection<IModelBall>();
        private int _radius;

        public ModelAPI()
        {
            _logicAPI = LogicAbstractAPI.CreateAPI();
        }

        public override ObservableCollection<IModelBall> GetModelBalls()
        {
            _modelBalls.Clear();
            foreach (IBall ball in _logicAPI.GetBalls())
            {
                IModelBall b = IModelBall.CreateModelBall((int)ball.Position.X, (int)ball.Position.Y, _radius);
                _modelBalls.Add(b);
                ball.ChangedPosition += b.UpdateModelBall!;
            }
            return _modelBalls;
        }

        public override void ClearBalls()
        {
            _logicAPI.ClearTable();
        }

        public override void Start(int n, int r)
        {
            _radius = r;
            _logicAPI.CreateBalls(n, r);
        }
    }
}
