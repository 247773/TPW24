using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;


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
