using System.Collections.ObjectModel;

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
                IModelBall b = IModelBall.CreateModelBall(ball.X, ball.Y, ball.R);
                _modelBalls.Add(b);
                ball.PropertyChanged += b.UpdateModelBall!;
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
