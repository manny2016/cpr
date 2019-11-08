

namespace OrgJoeyCommonUnitTest
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Org.Joey.Common;
    using OrgJoeyCommonUnitTest.Models;
    using System.Collections.Generic;
    using System.Linq;
    [TestClass]
    public class SortedTreeUnitTest
    {
        [TestMethod]
        public void TraverseSortedTree()
        {
            var prefix = new string[] { "a", "b", "c", "d", "e", "f", "g", "h" };
            var items = new List<SortedTreeNodeDataModel>();
            for (var i = 1; i <= 10; i++)
            {
                items.Add(new SortedTreeNodeDataModel($"{prefix[prefix.Length % i]}-{i}"));
            }
            var sortedTree = SortedTreeFactory.Create(items.ToArray());
            Assert.AreEqual(items.Count, sortedTree.ToList().Count());
        }
        [TestInitialize]
        public void Initialize()
        {

        }

        [TestCleanup]
        public void Cleanup()
        {

        }
    }
}
