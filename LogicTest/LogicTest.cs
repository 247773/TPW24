using Data;
using Logic;

namespace LogicTest
{
    internal class FakeDataBall : IDataBall
    {
        private BallPosition _position = new BallPosition(0, 0);
        public override BallPosition Position { get => _position; }

        private BallVelocity _velocity = new BallVelocity(0, 0);
        public override BallVelocity Velocity { get => _velocity; set => _velocity = value; }

        public override int ID { get; }
        public override float Time { get; set; }
        public override event EventHandler<DataEventArgs> ChangedPosition;

        public override void Dispose()
        {
            throw new NotImplementedException();
        }
    }

    internal class FakeDataAPI : IDataTable
    {
        public FakeDataAPI(int length, int width)
        {
            Length = length;
            Width = width;
        }

        public override int Length { get; }

        public override int Width { get; }

        public override IDataBall CreateDataBall(float x, float y, int r, int m, float vX, float vY, int id)
        {
            return new FakeDataBall();
        }

        public override List<IDataBall> GetBalls()
        {
            return new List<IDataBall>();
        }

        public override void ClearTable()
        {
            return;
        }
    }

    public class Tests
    {
        private IDataTable _data;
        private LogicAbstractAPI _table;

        [SetUp]
        public void Init()
        {
            _data = new FakeDataAPI(500, 500);
            _table = LogicAbstractAPI.CreateAPI(_data);
        }

        [Test]
        public void ConstructorTest()
        {
            Assert.IsNotNull(_table);
        }

        [Test]
        public void CreateBallsTest()
        {
            _table.CreateBalls(3, 5);
            Assert.AreEqual(_table.GetBalls().Count, 3);
        }

        [Test]
        public void ClearTableTest()
        {
            _table.CreateBalls(3, 5);
            Assert.AreEqual(_table.GetBalls().Count, 3);

            _table.ClearTable();
            Assert.AreEqual(_table.GetBalls().Count, 0);
        }

        [Test]
        public void ClearingEmptyTableTest()
        {
            _table.ClearTable();
            Assert.AreEqual(_table.GetBalls().Count, 0);
        }
    } 
}