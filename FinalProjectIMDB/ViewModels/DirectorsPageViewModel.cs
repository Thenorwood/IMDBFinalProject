using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using FinalProjectIMDB.Models;
using FinalProjectIMDB.Data;
using System.Data.Entity;

namespace FinalProjectIMDB.ViewModels
{
    public class DirectorsPageViewModel : INotifyPropertyChanged
    {

        private readonly ImdbContext _context = new();
        
        private int _totalDirectors = 0;

        private ObservableCollection<Director> _directors;
        private ObservableCollection<Director> Directors
        {
            get { return _directors; }
            set
            {
                _directors = value;
                OnPropertyChanged(nameof(Directors));
            }
        }

        private ObservableCollection<Director> _filteredDirectors;
        public ObservableCollection<Director> FilteredDirectors
        {
            get { return _filteredDirectors; }
            set
            {
                _filteredDirectors = value;
                OnPropertyChanged(nameof(FilteredDirectors));
            }
        }

        private ObservableCollection<Director> _randomDirectors;
        public ObservableCollection<Director> RandomDirectors
        {
            get { return _randomDirectors; }
            set
            {
                _randomDirectors = value;
                OnPropertyChanged(nameof(RandomDirectors));
            }
        }

        public DirectorsPageViewModel()
        {
            var allDirectors = _context.Directors.AsNoTracking().ToList();
            Directors = new ObservableCollection<Director>(allDirectors);
            FilteredDirectors = new ObservableCollection<Director>(Directors);
            RandomDirectors = new ObservableCollection<Director>(Directors.OrderBy(a => Guid.NewGuid()).Take(10));
        }

        private List<Director> GetRandomDirectors(List<Director> directors, int count)
        {
            // Make sure we don't try to take more than available
            count = Math.Min(count, directors.Count);

            // Shuffle and take N directors
            return directors.OrderBy(x => Guid.NewGuid()).Take(count).ToList();
        }

        public void RefreshRandomDirectors(int count = 10)
        {
            var randomDirectors = GetRandomDirectors(Directors.ToList(), count);
            RandomDirectors = new ObservableCollection<Director>(randomDirectors);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}