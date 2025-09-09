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
using NewTechnologies.Model;
using NewTechnologies.Pages;

namespace NewTechnologies
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();

            var partners = new_technologiesEntities.GetContext().partners.ToList();
            ListViewRequest.ItemsSource = partners;
            DataContext = this;
        }

        private void btnAddRequest_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddRequest());
        }

        private void btnAddPartner_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddEditPartner(null));
        }

        private void ListViewRequest_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (ListViewRequest.SelectedItem is partners selectedPartner)
            {
                if (selectedPartner != null)
                {
                    NavigationService.Navigate(new AddEditPartner(selectedPartner));
                }
                else
                {
                    MessageBox.Show($"Партнер не найден", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var context = new_technologiesEntities.GetContext();
            if (Visibility == Visibility.Visible)
            {
                context.ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                ListViewRequest.ItemsSource = context.partners.ToList();
            }
        }
    }
}
