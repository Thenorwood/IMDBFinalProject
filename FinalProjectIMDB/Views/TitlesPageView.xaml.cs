using FinalProjectIMDB.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FinalProjectIMDB.Views
{
    /// <summary>
    /// Interaction logic for TitlesPageView.xaml
    /// </summary>
    public partial class TitlesPageView : UserControl
    {
        public TitlesPageView()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void TestBinding_Click(object sender, RoutedEventArgs e)
        {
            var vm = (TitlesPageViewModel)DataContext;

            // Check if ViewModel is null
            if (vm == null)
            {
                MessageBox.Show("ViewModel is null - DataContext not set properly");
                return;
            }

            // Check if collection is null
            if (vm.FilteredTitles == null)
            {
                MessageBox.Show("FilteredTitles is null");
                return;
            }

            // Check count
            MessageBox.Show($"Found {vm.FilteredTitles.Count} items");

            // Display first 5 items if available
            if (vm.FilteredTitles.Count > 0)
            {
                var sample = string.Join("\n", vm.FilteredTitles.Take(5).Select(x => x.PrimaryTitle));
                MessageBox.Show($"First 5 items:\n{sample}");
            }
        }
    }
}
