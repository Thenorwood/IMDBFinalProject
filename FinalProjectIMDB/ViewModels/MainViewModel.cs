using System;
using System.Collections.Generic;
using System.ComponentModel;
using FinalProjectIMDB.Services;
using FinalProjectIMDB.Commands;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FinalProjectIMDB.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private object _currentViewModel;
        public object CurrentViewModel
        {
            get { return _currentViewModel; }
            set
            {
                _currentViewModel = value;
                OnPropertyChanged(nameof(CurrentViewModel));
            }
        }

        private readonly INavigationService _navigationService;
        public MainViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            CurrentViewModel = new HomePageViewModel();
        }

        public ICommand NavigateToHomePageCommand => new RelayCommand(_ => _navigationService.NavigateTo<HomePageViewModel>());
        public ICommand NavigateToDirectorsPageCommand => new RelayCommand(_ => _navigationService.NavigateTo<DirectorsPageViewModel>());
        public ICommand NavigateToGenresPageCommand => new RelayCommand(_ => _navigationService.NavigateTo<GenresPageViewModel>());
        public ICommand NavigateToTitlesPageCommand => new RelayCommand(_ => _navigationService.NavigateTo<TitlesPageViewModel>());

        public ICommand GoBackCommand => new RelayCommand(_ => _navigationService.GoBack());



        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
