using Data;
using System.Numerics;

namespace DataTests
{
    public class DataTests
    {
        IDataTable table = IDataTable.CreateAPI();

        [Test]
        public void CreateBallTest()
        {
            IDataBall dataBall = table.CreateDataBall(0, 0, 2, 1, 1, 1, 1);
            Assert.IsNotNull(dataBall);
        }

        [Test]
        public void BallVelocityTest()
        {
            IDataBall dataBall = table.CreateDataBall(1, 2, 3, 4, 5, 6 ,1);
            IDataBall.BallVelocity velocity = new IDataBall.BallVelocity(5, 6);
            Assert.AreEqual(dataBall.Velocity, velocity);

        }

        [Test]
        public void RemoveBallsTest()
        {
            IDataTable DataAPI = IDataTable.CreateAPI(400, 580);
            IDataBall dataBall = DataAPI.CreateDataBall(1, 1, 1, 1, 1, 1, 1);
            IDataBall dataBall2 = DataAPI.CreateDataBall(2, 2, 2, 2, 2, 2, 2);
            Assert.AreEqual(DataAPI.GetBalls().Count, 2);
            DataAPI.ClearTable();
            Assert.AreEqual(DataAPI.GetBalls().Count, 0);
        }
    }
}
