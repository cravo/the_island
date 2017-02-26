using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TheIsland;
using System.Threading;

namespace TheIslandTests
{
    [TestClass]
    public class MapTests
    {
        [TestMethod]
        public void TestConstructionIsntGenerated()
        {
            var map = new Map(10, 10);
            Assert.AreEqual(map.Generated, false);
        }

        [TestMethod]
        [Timeout(30000)]    // Assumes map generates within 30 seconds, increase if necessary
        public void TestMapGenerateSucceeds()
        {
            var map = new Map(4096, 4096);

            map.BeginGeneration();

            while(!map.Generated)
            {
                Thread.Sleep(100);
            }

            Assert.AreEqual(map.Generated, true);
            Assert.AreEqual(map.GenerationProgress, 1.0f);
        }

        [TestMethod]
        [ExpectedException(typeof(System.NullReferenceException))]
        public void TestGetMapDataAtWhenNotGenerated()
        {
            var map = new Map(4096, 4096);

            var data = map.GetMapDataAt(20, 34);
        }

        [TestMethod]
        [Timeout(30000)]
        public void TestGetMapDataAtWhenGenerated()
        {
            var map = new Map(4096, 4096);
            map.BeginGeneration();
            while(!map.Generated)
            {

            }

            Random rand = new Random();
            var data = map.GetMapDataAt(rand.Next(4096), rand.Next(4096));
            Assert.AreNotEqual(data, null);

            var originData = map.GetMapDataAt(0, 0);
            Assert.AreNotEqual(data, null);

            var limitsData = map.GetMapDataAt(4095, 4095);
            Assert.AreNotEqual(data, null);
        }

        [TestMethod]
        [Timeout(30000)]
        [ExpectedException(typeof(System.IndexOutOfRangeException))]
        public void TestGetMapDataOutOfBoundsNegative()
        {
            var map = new Map(4096, 4096);
            map.BeginGeneration();
            while (!map.Generated)
            {

            }

            Random rand = new Random();
            var data = map.GetMapDataAt(-rand.Next(4096), -rand.Next(4096));
        }

        [TestMethod]
        [Timeout(30000)]
        [ExpectedException(typeof(System.IndexOutOfRangeException))]
        public void TestGetMapDataOutOfBoundsPositive()
        {
            var map = new Map(4096, 4096);
            map.BeginGeneration();
            while (!map.Generated)
            {

            }

            Random rand = new Random();
            var data = map.GetMapDataAt(4096 + rand.Next(4096), 4096 + rand.Next(4096));
        }

        [TestMethod]
        [Timeout(30000)]
        public void TestMapHeightInRange()
        {
            var map = new Map(256, 256);
            map.BeginGeneration();
            while (!map.Generated) { }

            for(var y = 0; y < 256; ++y)
            {
                for(var x = 0; x < 256; ++x)
                {
                    var data = map.GetMapDataAt(x, y);
                    Assert.IsTrue(data.Height >= 0.0f);
                    Assert.IsTrue(data.Height <= 1.0f);
                }
            }
        }
    }
}
