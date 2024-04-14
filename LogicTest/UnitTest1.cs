using Logic;

namespace LogicTest
{
    public class BallTests
    {
        IBall _ball = IBall.CreateBall(1, 2, 3);

        [Test]
        public void ConstructorTest()
        {
            Assert.That(_ball, Is.Not.Null);
        }

        [Test]
        public void CreateBallTest()
        {
            Assert.AreEqual(1, _ball.X);
            Assert.AreEqual(2, _ball.Y);
            Assert.AreEqual(3, _ball.R);
        }

        [Test]
        public void SetBallPositionTest()
        {
            _ball.X = 5;
            _ball.Y = 7;
            Assert.AreEqual(5, _ball.X);
            Assert.AreEqual(7, _ball.Y);
        }

        [Test]
        public void SetBallRadiusTest()
        {
            _ball.R = 6;
            Assert.AreEqual(6, _ball.R);
        }

        private LogicAbstractAPI _logicAPI;

        [Test]
        public void TestCreateBalls()
        {
            _logicAPI = LogicAbstractAPI.CreateLogicAPI();
            _logicAPI.CreateBalls(3, 1);
            Assert.That(_logicAPI.GetBalls().Count, Is.EqualTo(3));
        }
    }
}