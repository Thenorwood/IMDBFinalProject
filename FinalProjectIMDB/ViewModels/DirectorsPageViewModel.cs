// DirectorsPageViewModel.cs
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using FinalProjectIMDB.Models;
using FinalProjectIMDB.Data;
using Microsoft.EntityFrameworkCore;
using FinalProjectIMDB.Commands;

namespace FinalProjectIMDB.ViewModels
{
    public class DirectorsPageViewModel : INotifyPropertyChanged
    {
        private readonly ImdbContext _context = new();
        private ObservableCollection<Director> _randomDirectors;

        public ObservableCollection<Director> RandomDirectors
        {
            get => _randomDirectors;
            set
            {
                _randomDirectors = value;
                OnPropertyChanged();
            }
        }

        public ICommand RefreshCommand { get; }

        public DirectorsPageViewModel()
        {
            // Updated RelayCommand initialization
            RefreshCommand = new RelayCommand(() => RefreshRandomDirectors());
            LoadRandomDirectors();
        }

        private void LoadRandomDirectors()
        {
            var allDirectors = _context.Directors
                .AsNoTracking()
                .Include(d => d.Title) 
                .ToList();

            RandomDirectors = new ObservableCollection<Director>(
                allDirectors.OrderBy(x => Guid.NewGuid()).Take(10));
        }

        public void RefreshRandomDirectors()
        {
            LoadRandomDirectors();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}