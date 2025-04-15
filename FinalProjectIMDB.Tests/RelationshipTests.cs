using Microsoft.VisualStudio.TestTools.UnitTesting;
using FinalProjectIMDB.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace FinalProjectIMDB.Tests
{
    [TestClass]
    public class RelationshipTests
    {
        private ImdbContext _context;

        [TestInitialize]
        public void Setup()
        {
            _context = new ImdbContext();
        }

        [TestCleanup]
        public void Cleanup()
        {
            _context.Dispose();
        }

        [TestMethod]
        public void Title_DirectorsRelationship_Works()
        {
            // Act - Get a title with its directors
            var title = _context.Titles
                .Include(t => t.Directors)
                .Where(t => t.Directors.Any())
                .FirstOrDefault();

            // Assert
            Assert.IsNotNull(title);
            Assert.IsTrue(title.Directors.Count > 0);

            // Check that each director references back to this title
            foreach (var director in title.Directors)
            {
                Assert.AreEqual(title.TitleId, director.TitleId);
            }
        }
    }
}
