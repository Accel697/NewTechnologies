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

        }

        private void btnAddPartner_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
