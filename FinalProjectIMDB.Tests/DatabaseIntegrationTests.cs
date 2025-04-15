using Microsoft.VisualStudio.TestTools.UnitTesting;
using FinalProjectIMDB.Data;
using FinalProjectIMDB.Models;
using System.Linq;

namespace FinalProjectIMDB.Tests
{
    [TestClass]
    public class DatabaseIntegrationTests
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
        public void CanConnectToDatabase()
        {
            // Assert
            Assert.IsTrue(_context.Database.CanConnect());
        }

        [TestMethod]
        public void CanRetrieveTitles()
        {
            // Act
            var titles = _context.Titles.Take(5).ToList();

            // Assert
            Assert.IsNotNull(titles);
            Assert.IsTrue(titles.Count > 0);

            // Verify title properties
            foreach (var title in titles)
            {
                Assert.IsFalse(string.IsNullOrEmpty(title.TitleId));
            }
        }

        [TestMethod]
        public void CanRetrieveDirectors()
        {
            // Act
            var directors = _context.Directors.Take(5).ToList();

            // Assert
            Assert.IsNotNull(directors);
            Assert.IsTrue(directors.Count > 0);

            // Verify director properties
            foreach (var director in directors)
            {
                Assert.IsFalse(string.IsNullOrEmpty(director.TitleId));
                Assert.IsFalse(string.IsNullOrEmpty(director.NameId));
            }
        }

        [TestMethod]
        public void CanRetrieveGenres()
        {
            // Act
            var genres = _context.Genres.ToList();

            // Assert
            Assert.IsNotNull(genres);
            Assert.IsTrue(genres.Count > 0);

            // Verify genre properties
            foreach (var genre in genres)
            {
                Assert.IsTrue(genre.GenreId > 0);
                Assert.IsFalse(string.IsNullOrEmpty(genre.Name));
            }
        }

        [TestMethod]
        public void CanRetrieveTitleWithDirectors()
        {
            // Act - Get a title that has directors
            var title = _context.Titles
                .Where(t => t.Directors.Any())
                .FirstOrDefault();

            // Assert
            Assert.IsNotNull(title);
            Assert.IsTrue(title.Directors.Count > 0);
        }
    }
}