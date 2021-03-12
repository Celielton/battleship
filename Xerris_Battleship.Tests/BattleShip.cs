using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Xerris_Battleship.Enumerators;
using Xerris_Battleship.Interfaces;
using Xerris_Battleship.Model;

namespace Xerris_Battleship.Tests
{
    [TestClass]
    public class BattleShip
    {
        [TestMethod]
        public void ShipNameShouldBeInvalid()
        {
            IShip shipOne = new Ship("", new List<Position>() { new Position(1, 'A'), new Position(1, 'B'), new Position(1, 'C') });
            Assert.IsFalse(shipOne.IsValid());
        }

        [TestMethod]
        public void ShipNameShouldBeValid()
        {
            IShip shipOne = new Ship("Ship one", new List<Position>() { new Position(1, 'A'), new Position(2, 'B'), new Position(3, 'C') });
            Assert.IsFalse(shipOne.IsValid());
        }

        [TestMethod]
        public void ShipPositionHorizontalShouldBeInvalid()
        {
            IShip shipOne = new Ship("Player A", new List<Position>() { new Position(1, 'A'), new Position(1, 'C'), new Position(1, 'Z') });
            Assert.IsFalse(shipOne.IsValid());
        }


        [TestMethod]
        public void ShipPositionHoriziontalShouldBeValid()
        {
            IShip shipOne = new Ship("Player A", new List<Position>() { new Position(1, 'A'), new Position(1, 'B'), new Position(1, 'C') });
            Assert.IsTrue(shipOne.IsValid());
        }

        [TestMethod]
        public void ShipPositionVerticalShouldBeValid()
        {
            IShip shipOne = new Ship("Ship one", new List<Position>() { new Position(1, 'A'), new Position(2, 'A'), new Position(3, 'A') });
            Assert.IsTrue(shipOne.IsValid());
        }

        [TestMethod]
        public void ShipPositionVerticalShouldBeInvalid()
        {
            IShip shipOne = new Ship("Ship one", new List<Position>() { new Position(1, 'A'), new Position(3,'A'), new Position(4, 'A') });
            Assert.IsFalse(shipOne.IsValid());
        }

        [TestMethod]
        public void ShipIsNotShunkWithConfigurations()
        {
            IShip shipOne = new Ship("Ship one", new List<Position>() { new Position(1, 'A'), new Position(2, 'A'), new Position(3, 'A') }, GameLevel.Medium);
            shipOne.CheckHit(new Position(1, 'A'));
            Assert.IsFalse(shipOne.IsSunk());
        }

        [TestMethod]
        public void ShipShouldSunkWithHint()
        {
            IShip shipOne = new Ship("Ship one", new List<Position>() { new Position(1, 'A'), new Position(2, 'A'), new Position(3, 'A') });
            shipOne.CheckHit(new Position(1, 'A'));
            Assert.IsTrue(shipOne.IsSunk());

        }


        [TestMethod]
        public void ShipShouldNotSunkWithoutHint()
        {
            IShip shipOne = new Ship("Ship one", new List<Position>() { new Position(1, 'A'), new Position(2, 'A'), new Position(3, 'A') }, GameLevel.Easy);
            shipOne.CheckHit(new Position(1, 'B'));
            Assert.IsFalse(shipOne.IsSunk());
        }

        [TestMethod]
        public void ShipShouldBeSunkWithEnoughtHints()
        {
            IShip shipOne = new Ship("Ship one", new List<Position>() { new Position(1, 'A'), new Position(2, 'A'), new Position(3, 'A') }, GameLevel.Medium);
            shipOne.CheckHit(new Position(1, 'A'));
            Assert.IsFalse(shipOne.IsSunk());

            shipOne.CheckHit(new Position(1, 'F'));
            Assert.IsFalse(shipOne.IsSunk());

            shipOne.CheckHit(new Position(2, 'A'));
            Assert.IsTrue(shipOne.IsSunk());
        }

        [TestMethod]
        public void ShipPositionShoulBeHorizontal()
        {
            Ship shipOne = new Ship("Player 1", new List<Position>() { new Position(1, 'A'), new Position(1, 'B'), new Position(1, 'C') });
            Assert.AreEqual(Orientation.Horizontal, shipOne.Orientation);
        }

        [TestMethod]
        public void ShipPositionShoulBeVertical()
        {
            Ship shipOne = new Ship("Player 1", new List<Position>() { new Position(3, 'A'), new Position(4, 'A'), new Position(5, 'A') });
            Assert.AreEqual(Orientation.Vertical, shipOne.Orientation);
        }

    }
}
