using System.Windows;
using FinalProjectIMDB.Views;

namespace FinalProjectIMDB
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new HomePage());
        }
    }
}