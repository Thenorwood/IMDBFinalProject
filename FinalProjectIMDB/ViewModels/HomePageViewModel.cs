using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using FinalProjectIMDB.Models;
using FinalProjectIMDB.Data;

namespace FinalProjectIMDB.ViewModels
{
    public class HomePageViewModel : INotifyPropertyChanged
    {
        private int _totalMovies;
        private int _totalDirectors;
        private int _totalGenres;

        public HomePageViewModel()
        {
            LoadSummaryData();
        }

        private void LoadSummaryData()
        {
            using (var context = new ImdbContext())
            {
                TotalMovies = context.Titles.Count();
                TotalDirectors = context.Directors.Count();
                TotalGenres = context.Genres.Count();
            }
        }

        public int TotalMovies
        {
            get { return _totalMovies; }
            set
            {
                _totalMovies = value;
                OnPropertyChanged();
            }
        }

        public int TotalDirectors
        {
            get { return _totalDirectors; }
            set
            {
                _totalDirectors = value;
                OnPropertyChanged();
            }
        }

        public int TotalGenres
        {
            get { return _totalGenres; }
            set
            {
                _totalGenres = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}