using MasterFloor_WpfApp.Services;

namespace MasterFloor_TestProject
{
    [TestFixture]
    public class PercentCalculatorTests
    {
        [TestCase(0, 0)]
        [TestCase(9999, 0)]
        [TestCase(10000, 5)]
        [TestCase(50000, 10)]
        [TestCase(300001, 15)]
        public void GetPercent_ReturnsExpected(int totalSale, int expected)
        {
            int actual = Service.GetPercent(totalSale);
            Assert.AreEqual(expected, actual);
        }
    }
}