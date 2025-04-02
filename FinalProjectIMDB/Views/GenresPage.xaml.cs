using System.Windows.Controls;
using FinalProjectIMDB.ViewModels;

namespace FinalProjectIMDB.Views
{
    public partial class GenresPage : Page
    {
        public GenresPage()
        {
            InitializeComponent();
            DataContext = new GenresPageViewModel();
        }
    }
}