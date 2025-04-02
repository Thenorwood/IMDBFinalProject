using System.Windows;
using System.Windows.Controls;
using FinalProjectIMDB.ViewModels;

namespace FinalProjectIMDB.Views
{
    public partial class HomePage : Page
    {
        public HomePage()
        {
            InitializeComponent();
            DataContext = new HomePageViewModel();
        }

        private void BrowseMovies_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new TitlesPage());
        }

        private void BrowseDirectors_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new DirectorsPage());
        }

        private void BrowseGenres_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new GenresPage());
        }
    }
}