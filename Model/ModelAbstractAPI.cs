using System.Collections.ObjectModel;

namespace Model
{
    public abstract class ModelAbstractAPI
    {
        public static ModelAbstractAPI CreateModelAPI()
        {
            return new ModelAPI();
        }

        public abstract void Start(int numOfBalls, int r);
        public abstract void ClearBalls();
        public abstract ObservableCollection<IModelBall> GetModelBalls();
    }
}