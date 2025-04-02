using System.Windows.Controls;
using FinalProjectIMDB.ViewModels;

namespace FinalProjectIMDB.Views
{
    public partial class DirectorsPage : Page
    {
        public DirectorsPage()
        {
            InitializeComponent();
            DataContext = new DirectorsPageViewModel();
        }
    }
}