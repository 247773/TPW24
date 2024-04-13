using System.Collections.ObjectModel;
using System.Threading;

using Logic;

namespace Model
{
    internal class ModelAPI : ModelAbstractAPI
    {
        private LogicAbstractAPI _logicAPI;
        private ObservableCollection<IModelBall> _modelBalls = new ObservableCollection<IModelBall>();

        public ModelAPI()
        {
            _logicAPI = LogicAbstractAPI.CreateLogicAPI();
        }

        public override ObservableCollection<IModelBall> GetModelBalls()
        {
            _modelBalls.Clear();
            foreach (IBall ball in _logicAPI.GetBalls())
            {
                IModelBall c = IModelBall.CreateModelBall(ball.X, ball.Y, ball.R);
                _modelBalls.Add(c);
                ball.PropertyChanged += c.UpdateModelBall!;
            }
            return _modelBalls;
        }

        public override void ClearBalls()
        {
            _logicAPI.ClearTable();
        }

        public override void Start(int numOfBalls, int r)
        {
            _logicAPI.CreateBalls(numOfBalls, r);
            _logicAPI.StartSimulation();
        }
    }
}
