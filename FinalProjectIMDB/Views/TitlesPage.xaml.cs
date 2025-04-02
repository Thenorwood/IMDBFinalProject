using System.Windows;
using System.Windows.Controls;
using FinalProjectIMDB.Models;
using FinalProjectIMDB.ViewModels;

namespace FinalProjectIMDB.Views
{
    public partial class TitlesPage : Page
    {
        public TitlesPage()
        {
            InitializeComponent();
            DataContext = new TitlesPageViewModel();
        }

        private void ViewDetails_Click(object sender, RoutedEventArgs e)
        {
            // Get the clicked movie
            var title = (sender as Button).DataContext as Title;

            // Show a simple message box with movie details
            MessageBox.Show($"Title: {title.PrimaryTitle}\nYear: {title.StartYear}\nRuntime: {title.RuntimeMinutes} min",
                           "Movie Details",
                           MessageBoxButton.OK);
        }
    }
}