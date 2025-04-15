using FinalProjectIMDB.Commands;
using FinalProjectIMDB.Data;
using FinalProjectIMDB.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace FinalProjectIMDB.ViewModels
{
    public class TitlesPageViewModel : INotifyPropertyChanged
    {
        private readonly ImdbContext _context;
        private string _searchTerm;
        private bool _isLoading;
        private string _statusMessage;

        public TitlesPageViewModel()
        {
            _context = new ImdbContext();
            Titles = new ObservableCollection<Title>();
            FilteredTitles = new ObservableCollection<Title>();
            LoadTitlesCommand = new RelayCommand(async () => await LoadTitlesAsync());

            // Initial load
            LoadTitlesCommand.Execute(null);
        }

        public ICommand LoadTitlesCommand { get; }
        public ICommand DetailCommand { get; }

        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                if (_isLoading != value)
                {
                    _isLoading = value;
                    OnPropertyChanged();
                }
            }
        }


        private string _debugText;
        public string DebugText
        {
            get => _debugText;
            set
            {
                if (_debugText != value)
                {
                    _debugText = value;
                    OnPropertyChanged();
                    Debug.WriteLine($"DebugText updated: {value}"); // Also log to debug output
                }
            }
        }


        public string StatusMessage
        {
            get => _statusMessage;
            set
            {
                if (_statusMessage != value)
                {
                    _statusMessage = value;
                    OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<Title> Titles { get; private set; }

        private ObservableCollection<Title> _filteredTitles;
        public ObservableCollection<Title> FilteredTitles
        {
            get => _filteredTitles;
            private set
            {
                if (_filteredTitles != value)
                {
                    _filteredTitles = value;
                    OnPropertyChanged();
                }
            }
        }

        public string SearchTerm
        {
            get => _searchTerm;
            set
            {
                if (_searchTerm != value)
                {
                    _searchTerm = value;
                    OnPropertyChanged();
                    FilterTitles();
                }
            }
        }

        private async Task LoadTitlesAsync()
        {
            try
            {
                IsLoading = true;
                StatusMessage = "Loading titles...";
                Debug.WriteLine("Starting to load titles from database...");

                await Task.Run(async () =>
                {
                    var titlesFromDb = await _context.Titles
                        .AsNoTracking()
                        .OrderBy(t => t.PrimaryTitle)
                        .ToListAsync();

                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        Titles = new ObservableCollection<Title>(titlesFromDb);
                        FilterTitles(); 
                        StatusMessage = $"Loaded {titlesFromDb.Count} titles";
                        Debug.WriteLine($"Loaded {titlesFromDb.Count} titles");
                    });
                });
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error loading titles: {ex}");
                StatusMessage = "Error loading titles";
                MessageBox.Show($"Error loading titles: {ex.Message}",
                    "Database Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                IsLoading = false;
            }
        }

        private void FilterTitles()
        {
            if (string.IsNullOrWhiteSpace(SearchTerm))
            {
                FilteredTitles = new ObservableCollection<Title>(Titles);
            }
            else
            {
                var searchLower = SearchTerm.ToLower();
                FilteredTitles = new ObservableCollection<Title>(
                    Titles.Where(t =>
                        t.PrimaryTitle?.ToLower().Contains(searchLower) == true)
                );
            }
            Debug.WriteLine($"Filtered titles count: {FilteredTitles.Count}");
        }

       

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}