using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using FinalProjectIMDB.Models;

namespace FinalProjectIMDB.ViewModels
{
    public class GenresPageViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Genre> _genres;

        public GenresPageViewModel()
        {
            LoadGenres();
        }

        private void LoadGenres()
        {
            using (var context = new ImdbContext())
            {
                var genresFromDb = context.Genres.ToList();
                _genres = new ObservableCollection<Genre>(genresFromDb);
            }
        }

        public ObservableCollection<Genre> Genres
        {
            get { return _genres; }
            set
            {
                _genres = value;
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