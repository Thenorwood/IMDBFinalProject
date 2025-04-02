using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Data;
using System.Windows.Input;
using FinalProjectIMDB.Models;

namespace FinalProjectIMDB.ViewModels
{
    public class TitlesPageViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Title> _titles;
        private ICollectionView _titlesView;

        // Filter properties
        private string _searchText;
        private int? _selectedGenreId;
        private int? _startYearFilter;
        private int? _endYearFilter;
        private ObservableCollection<Genre> _genres;

        public TitlesPageViewModel()
        {
            LoadData();

            ResetFiltersCommand = new RelayCommand(param => ResetFilters());
            ApplyFiltersCommand = new RelayCommand(param => ApplyFilters());
        }

        private void LoadData()
        {
            // Use the database context to load data
            using (var context = new ImdbContext())
            {
                // Load titles (limit to 100 for performance)
                var titlesFromDb = context.Titles.Take(100).ToList();
                _titles = new ObservableCollection<Title>(titlesFromDb);

                // Load genres
                var genresFromDb = context.Genres.ToList();
                _genres = new ObservableCollection<Genre>(genresFromDb);

                // Add "All Genres" option
                var allGenre = new Genre { GenreId = 0, Name = "All Genres" };
                _genres.Insert(0, allGenre);
            }

            _titlesView = CollectionViewSource.GetDefaultView(_titles);
            SelectedGenreId = 0; // Default to "All Genres"
        }

        public ObservableCollection<Title> Titles
        {
            get { return _titles; }
            set
            {
                _titles = value;
                OnPropertyChanged();
            }
        }

        public ICollectionView TitlesView
        {
            get { return _titlesView; }
        }

        public ObservableCollection<Genre> Genres
        {
            get { return _genres; }
        }

        public string SearchText
        {
            get { return _searchText; }
            set
            {
                _searchText = value;
                OnPropertyChanged();
            }
        }

        public int? SelectedGenreId
        {
            get { return _selectedGenreId; }
            set
            {
                _selectedGenreId = value;
                OnPropertyChanged();
            }
        }

        public int? StartYearFilter
        {
            get { return _startYearFilter; }
            set
            {
                _startYearFilter = value;
                OnPropertyChanged();
            }
        }

        public int? EndYearFilter
        {
            get { return _endYearFilter; }
            set
            {
                _endYearFilter = value;
                OnPropertyChanged();
            }
        }

        public ICommand ResetFiltersCommand { get; }
        public ICommand ApplyFiltersCommand { get; }

        private void ResetFilters()
        {
            SearchText = null;
            SelectedGenreId = 0;
            StartYearFilter = null;
            EndYearFilter = null;

            _titlesView.Filter = null;
        }

        private void ApplyFilters()
        {
            _titlesView.Filter = item =>
            {
                var title = item as Title;
                bool match = true;

                // Apply text search filter
                if (!string.IsNullOrEmpty(SearchText))
                {
                    match = title.PrimaryTitle.IndexOf(SearchText, StringComparison.OrdinalIgnoreCase) >= 0 ||
                            title.OriginalTitle.IndexOf(SearchText, StringComparison.OrdinalIgnoreCase) >= 0;
                }

                // Apply genre filter - this is a simplification since we don't have direct genre linkage
                // In a real implementation, you would use the proper relationship
                if (match && SelectedGenreId > 0)
                {
                    // Since we don't have direct Title-Genre relationship here,
                    // this part would need to be implemented based on your actual database schema
                    using (var context = new ImdbContext())
                    {
                        // This is a placeholder - would need proper implementation
                        match = true; // Always true for now
                    }
                }

                // Apply year range filter
                if (match && StartYearFilter.HasValue)
                {
                    match = title.StartYear >= StartYearFilter.Value;
                }

                if (match && EndYearFilter.HasValue)
                {
                    match = title.EndYear <= EndYearFilter.Value ||
                            (title.EndYear == null && title.StartYear <= EndYearFilter.Value);
                }

                return match;
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    // Basic implementation of ICommand for binding commands in XAML
    public class RelayCommand : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Predicate<object> _canExecute;

        public RelayCommand(Action<object> execute, Predicate<object> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            _execute(parameter);
        }
    }
}