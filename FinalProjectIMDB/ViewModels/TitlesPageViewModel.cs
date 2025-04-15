using FinalProjectIMDB.Data;
using FinalProjectIMDB.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace FinalProjectIMDB.ViewModels
{
    public class TitlesPageViewModel : INotifyPropertyChanged
    {
        private readonly ImdbContext _context = new();
        private string _searchTerm;
        private int? _startYearFilter;
        private int? _endYearFilter;

        private ObservableCollection<Title> _titles;
        public ObservableCollection<Title> Titles
        {
            get => _titles;
            set
            {
                _titles = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Title> _filteredTitles;
        public ObservableCollection<Title> FilteredTitles
        {
            get => _filteredTitles;
            set
            {
                _filteredTitles = value;
                OnPropertyChanged();
            }
        }

        public string SearchTerm
        {
            get => _searchTerm;
            set
            {
                _searchTerm = value;
                OnPropertyChanged();
                FilterTitles();
            }
        }

        public int? StartYearFilter
        {
            get => _startYearFilter;
            set
            {
                _startYearFilter = value;
                OnPropertyChanged();
                FilterTitles();
            }
        }

        public int? EndYearFilter
        {
            get => _endYearFilter;
            set
            {
                _endYearFilter = value;
                OnPropertyChanged();
                FilterTitles();
            }
        }

        public TitlesPageViewModel()
        {
            LoadTitles();
        }

        private void LoadTitles()
        {
            var titles = _context.Titles
                .AsNoTracking()
                .OrderBy(t => t.PrimaryTitle)
                .Take(500) // Initial load size
                .ToList();

            Titles = new ObservableCollection<Title>(titles);
            FilteredTitles = new ObservableCollection<Title>(Titles);
        }

        private void FilterTitles()
        {
            var filtered = Titles.AsEnumerable();

            if (!string.IsNullOrWhiteSpace(SearchTerm))
            {
                var term = SearchTerm.ToLower();
                filtered = filtered.Where(t =>
                    (t.PrimaryTitle != null && t.PrimaryTitle.ToLower().Contains(term)) ||
                    (t.OriginalTitle != null && t.OriginalTitle.ToLower().Contains(term)));
            }

            if (StartYearFilter.HasValue)
            {
                filtered = filtered.Where(t => t.StartYear >= StartYearFilter.Value);
            }

            if (EndYearFilter.HasValue)
            {
                filtered = filtered.Where(t =>
                    t.EndYear <= EndYearFilter.Value ||
                    (t.EndYear == null && t.StartYear <= EndYearFilter.Value));
            }

            FilteredTitles = new ObservableCollection<Title>(filtered);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}