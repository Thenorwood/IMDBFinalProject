using Microsoft.VisualStudio.TestTools.UnitTesting;
using FinalProjectIMDB.Models;
using System.Collections.Generic;

namespace FinalProjectIMDB.Tests
{
    [TestClass]
    public class ModelTests
    {
        [TestMethod]
        public void Title_PropertiesInitializeCorrectly()
        {
            // Arrange
            var title = new Title
            {
                TitleId = "tt0000001",
                TitleType = "movie",
                PrimaryTitle = "Test Movie",
                OriginalTitle = "Test Original",
                IsAdult = false,
                StartYear = 2020,
                EndYear = null,
                RuntimeMinutes = 120
            };

            // Assert
            Assert.AreEqual("tt0000001", title.TitleId);
            Assert.AreEqual("movie", title.TitleType);
            Assert.AreEqual("Test Movie", title.PrimaryTitle);
            Assert.AreEqual("Test Original", title.OriginalTitle);
            Assert.AreEqual(false, title.IsAdult);
            Assert.AreEqual((short)2020, title.StartYear);
            Assert.IsNull(title.EndYear);
            Assert.AreEqual((short)120, title.RuntimeMinutes);
        }

        [TestMethod]
        public void Director_PropertiesInitializeCorrectly()
        {
            // Arrange
            var title = new Title { TitleId = "tt0000001" };
            var director = new Director
            {
                TitleId = "tt0000001",
                NameId = "nm0000001",
                Title = title
            };

            // Assert
            Assert.AreEqual("tt0000001", director.TitleId);
            Assert.AreEqual("nm0000001", director.NameId);
            Assert.AreEqual(title, director.Title);
        }

        [TestMethod]
        public void Genre_PropertiesInitializeCorrectly()
        {
            // Arrange
            var genre = new Genre
            {
                GenreId = 1,
                Name = "Action"
            };

            // Assert
            Assert.AreEqual(1, genre.GenreId);
            Assert.AreEqual("Action", genre.Name);
        }
    }
}