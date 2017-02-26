using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TheIsland;

namespace TheIslandTests
{
    [TestClass]
    public class MapTests
    {
        [TestMethod]
        public void TestConstructionIsntGenerated()
        {
            Map map = new Map(10, 10);
            Assert.AreEqual(map.Generated, false);
        }

        [TestMethod]
        [Timeout(30000)]    // Assumes map generates within 30 seconds
        public void TestMapGenerateSucceeds()
        {
            Map map = new Map(4096, 4096);

            map.BeginGeneration();

            while(!map.Generated)
            {

            }

            Assert.AreEqual(map.Generated, true);
            Assert.AreEqual(map.GenerationProgress, 1.0f);
        }
    }
}
