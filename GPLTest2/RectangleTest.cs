
using GPLApp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPLTest2
{
    [TestClass]
    public class RectangleTest
    {
        /// <summary>
        /// Method to test rectangle while providing manaul params like x,y axis along with height and width and checking it with main program
        /// </summary>
        [TestMethod]
        public void TestRect()
        {
            var r = new Rectangle();
            int x = 100, y = 50, height = 80, width = 75;
            r.Set(x, y, height, width);
            Assert.AreEqual(100, r.x); //Assert.AreEqual(expected,actual)
        }

    }
}
