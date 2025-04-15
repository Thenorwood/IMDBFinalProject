// MainViewModel.cs
using System.ComponentModel;
using FinalProjectIMDB.Services;
using FinalProjectIMDB.ViewModels;
using FinalProjectIMDB.Commands;
using System.Windows.Input;

namespace FinalProjectIMDB.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private object _currentViewModel;
        public object CurrentViewModel
        {
            get => _currentViewModel;
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

            // Initialize commands with proper parameter handling
            NavigateToHomePageCommand = new RelayCommand(() => _navigationService.NavigateTo<HomePageViewModel>());
            NavigateToDirectorsPageCommand = new RelayCommand(() => _navigationService.NavigateTo<DirectorsPageViewModel>());
            NavigateToGenresPageCommand = new RelayCommand(() => _navigationService.NavigateTo<GenresPageViewModel>());
            NavigateToTitlesPageCommand = new RelayCommand(() => _navigationService.NavigateTo<TitlesPageViewModel>());
            GoBackCommand = new RelayCommand(() => _navigationService.GoBack());
        }

        public ICommand NavigateToHomePageCommand { get; }
        public ICommand NavigateToDirectorsPageCommand { get; }
        public ICommand NavigateToGenresPageCommand { get; }
        public ICommand NavigateToTitlesPageCommand { get; }
        public ICommand GoBackCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}