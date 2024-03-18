using TPW;

namespace TPWTests
{
    public class SampleClassTest
    {
        [Test]
        public void addTest()
        {
            int x = 4;
            int y = 5;
            int result1 = SampleClass.add(x, y);
            int result2 = SampleClass.add(y, x);
            Assert.AreEqual(9, result1);
            Assert.AreEqual(result1, result2);
        }
    }
}