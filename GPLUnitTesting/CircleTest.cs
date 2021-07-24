using GPLApp;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GPLUnitTesting
{
    [TestClass]
    public class CircleTest
    {
        /// <summary>
        /// Method to test the circle while providing manual parameters which checks it with our main program
        /// </summary>
        [TestMethod]
        public void TestCircle()
        {
            var r = new Circle();
            int x = 100, y = 50, radius = 40;
            r.Set(x, y, radius);
            Assert.AreEqual(100, r.x); //Assert.AreEqual(expected,actual)
        }
    }
}
