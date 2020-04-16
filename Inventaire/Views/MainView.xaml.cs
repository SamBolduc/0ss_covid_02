using app_models;
using BillingManagement.UI.ViewModels;
using System.Windows;

namespace Inventaire
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainView : Window
    {
        public MainView()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }

        private void CustomerNew_Click(object sender, RoutedEventArgs e) {}

        private void CustomerDelete_Click(object sender, RoutedEventArgs e) {}

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }
    }
}
