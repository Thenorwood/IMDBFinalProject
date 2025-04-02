using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using FinalProjectIMDB.Models;

namespace FinalProjectIMDB.ViewModels
{
    public class DirectorsPageViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Director> _directors;

        public DirectorsPageViewModel()
        {
            LoadDirectors();
        }

        private void LoadDirectors()
        {
            using (var context = new ImdbContext())
            {
                var directorsFromDb = context.Directors.Take(100).ToList();
                _directors = new ObservableCollection<Director>(directorsFromDb);
            }
        }

        public ObservableCollection<Director> Directors
        {
            get { return _directors; }
            set
            {
                _directors = value;
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