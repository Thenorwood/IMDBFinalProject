using Microsoft.VisualStudio.TestTools.UnitTesting;
using FinalProjectIMDB.Data;
using FinalProjectIMDB.ViewModels;
using System.Linq;

namespace FinalProjectIMDB.Tests
{
    [TestClass]
    public class EndToEndTests
    {
        [TestMethod]
        public void HomePageViewModel_ShowsCorrectCounts()
        {
            // Arrange
            using (var context = new ImdbContext())
            {
                var expectedTitleCount = context.Titles.Count();
                var expectedDirectorCount = context.Directors.Count();
                var expectedGenreCount = context.Genres.Count();

                // Act
                var viewModel = new HomePageViewModel();

                // Assert
                Assert.AreEqual(expectedTitleCount, viewModel.TotalMovies);
                Assert.AreEqual(expectedDirectorCount, viewModel.TotalDirectors);
                Assert.AreEqual(expectedGenreCount, viewModel.TotalGenres);
            }
        }
    }
}