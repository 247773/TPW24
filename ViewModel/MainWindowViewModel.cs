using Model;
using System.Collections.ObjectModel;

namespace ViewModel
{
    public class MainWindowViewModel
    {
        private ModelAbstractAPI _modelAPI;

        public ObservableCollection<IModelBall> _modelBalls => _modelAPI.GetModelBalls();

        public RelayCommand Start { get; }

        public RelayCommand Stop { get;  }

        private String _BallsAmount = "";

        private int _ballRadius = 10;

        public String NumOfBalls
        {
            get => _BallsAmount;
            set
            {
                _BallsAmount = value;
                RaisePropertyChanged();
            }
        }

        public MainWindowViewModel()
        {
            _modelAPI = ModelAbstractAPI.CreateModelAPI();
            Start = new RelayCommand(StartProcess);
            Stop = new RelayCommand(StopProcess);
        }

        public void StartProcess()
        {
            int BallsAmountInt = int.Parse(NumOfBalls);
            _modelAPI.Start(BallsAmountInt, _ballRadius);
            RaisePropertyChanged("_modelBalls");
        }

        public void StopProcess()
        {
            _modelAPI.ClearBalls();
            RaisePropertyChanged("_modelBalls");
        }
    }
}
