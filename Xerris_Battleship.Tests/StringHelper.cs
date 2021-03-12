using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xerris_Battleship.Helpers;

namespace Xerris_Battleship.Tests
{
    [TestClass]
    public class StringHelper
    {
        [TestMethod]
        public void PositionShoulBeValid()
        {
            var position = "A1-B1-C1";
            Assert.IsTrue(position.IsValidPosition());
        }

        [TestMethod]
        public void PositionShoulBeInvalid()
        {
            var position = "A1-1B-C1";
            Assert.IsFalse(position.IsValidPosition());
        }
    }
}
