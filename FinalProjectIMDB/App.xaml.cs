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
        navigationService.SetMainViewModel(mainViewModel);

        var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();

        mainWindow.Show();
    }

    private void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<MainWindow>();
        services.AddSingleton<MainViewModel>();
        services.AddSingleton<INavigationService, NavigationService>();
        services.AddSingleton<HomePageViewModel>();
        services.AddSingleton<DirectorsPageViewModel>();
        services.AddSingleton<GenresPageViewModel>();
        services.AddSingleton<TitlesPageViewModel>();

        services.AddDbContext<ImdbContext>(options =>
            options.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=IMDB_Project;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False"));
    }


    private void LoadData()
    {
        using (var scope = ServiceProvider.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<ImdbContext>();

            var directorsViewModel = ServiceProvider.GetRequiredService<DirectorsPageViewModel>();
            var genresViewModel = ServiceProvider.GetRequiredService<GenresPageViewModel>();
            var titlesViewModel = ServiceProvider.GetRequiredService<TitlesPageViewModel>();

            // Fix for CS0200: Use the existing ObservableCollection and clear/add items instead of reassigning
            directorsViewModel.RandomDirectors.Clear();
            foreach (var director in dbContext.Directors.ToList())
            {
                directorsViewModel.RandomDirectors.Add(director);
            }

            genresViewModel.Genres.Clear();
            foreach (var genre in dbContext.Genres.ToList())
            {
                genresViewModel.Genres.Add(genre);
            }

            titlesViewModel.Titles.Clear();
            foreach (var title in dbContext.Titles.ToList())
            {
                titlesViewModel.Titles.Add(title);
            }
        }
    }
}

