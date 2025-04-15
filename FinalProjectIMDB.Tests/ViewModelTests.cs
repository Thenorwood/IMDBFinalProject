using Microsoft.VisualStudio.TestTools.UnitTesting;
using FinalProjectIMDB.ViewModels;
using System.ComponentModel;
using System.Collections.Generic;

namespace FinalProjectIMDB.Tests
{
    [TestClass]
    public class ViewModelTests
    {
        [TestMethod]
        public void HomePageViewModel_LoadsData()
        {
            // Arrange & Act
            var viewModel = new HomePageViewModel();

            // Assert - Just verify that counts are non-negative
            Assert.IsTrue(viewModel.TotalMovies >= 0);
            Assert.IsTrue(viewModel.TotalDirectors >= 0);
            Assert.IsTrue(viewModel.TotalGenres >= 0);
        }

        [TestMethod]
        public void HomePageViewModel_PropertyChangedWorks()
        {
            // Arrange
            var viewModel = new HomePageViewModel();
            var propertiesChanged = new List<string>();

            viewModel.PropertyChanged += (sender, e) => {
                propertiesChanged.Add(e.PropertyName);
            };

            // Act
            viewModel.TotalMovies = 100;
            viewModel.TotalDirectors = 50;
            viewModel.TotalGenres = 20;

            // Assert
            Assert.IsTrue(propertiesChanged.Contains("TotalMovies"));
            Assert.IsTrue(propertiesChanged.Contains("TotalDirectors"));
            Assert.IsTrue(propertiesChanged.Contains("TotalGenres"));
        }

        [TestMethod]
        public void GenresPageViewModel_Initializes()
        {
            // Arrange & Act - Just verify it doesn't throw an exception
            var viewModel = new GenresPageViewModel();

            // Assert
            Assert.IsNotNull(viewModel);
        }

        [TestMethod]
        public void DirectorsPageViewModel_Initializes()
        {
            // Arrange & Act - Just verify it doesn't throw an exception
            var viewModel = new DirectorsPageViewModel();

            // Assert
            Assert.IsNotNull(viewModel);
        }

        [TestMethod]
        public void TitlesPageViewModel_Initializes()
        {
            // Arrange & Act - Just verify it doesn't throw an exception
            var viewModel = new TitlesPageViewModel();

            // Assert
            Assert.IsNotNull(viewModel);
        }
    }
}