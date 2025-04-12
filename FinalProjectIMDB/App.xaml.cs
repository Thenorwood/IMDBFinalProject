using FinalProjectIMDB.Data;
using FinalProjectIMDB.Models;
using FinalProjectIMDB.Services;
using FinalProjectIMDB.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Windows;

namespace FinalProjectIMDB;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    public IServiceProvider ServiceProvider { get; private set; }

    private void Application_Startup(object sender, StartupEventArgs e)
    {
        var services = new ServiceCollection();
        ConfigureServices(services);
        ServiceProvider = services.BuildServiceProvider();

        LoadData();

        var mainViewModel = ServiceProvider.GetRequiredService<MainViewModel>();
        var navigationService = ServiceProvider.GetRequiredService<INavigationService>() as NavigationService;

        var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();

        mainWindow.Show();
    }

    private void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<MainWindow>();
        services.AddSingleton<MainViewModel>();
        services.AddSingleton<INavigationService, NavigationService>();
        services.AddTransient<HomePageViewModel>();
        services.AddTransient<DirectorsPageViewModel>();
        services.AddTransient<GenresPageViewModel>();
        services.AddTransient<TitlesPageViewModel>();

        services.AddDbContext<ImdbContext>(options =>
            options.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=IMDB;Trusted_Connection=True;"));
    }


    private void LoadData()
    {
        using (var scope = ServiceProvider.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<ImdbContext>();

            var directorsViewModel = ServiceProvider.GetRequiredService<DirectorsPageViewModel>();
            var genresViewModel = ServiceProvider.GetRequiredService<GenresPageViewModel>();
            var titlesViewModel = ServiceProvider.GetRequiredService<TitlesPageViewModel>();

            directorsViewModel.Directors = new ObservableCollection<Director>(dbContext.Directors.ToList());
            genresViewModel.Genres = new ObservableCollection<Genre>(dbContext.Genres.ToList());
            titlesViewModel.Titles = new ObservableCollection<Title>(dbContext.Titles.ToList());
        }
    }
}

