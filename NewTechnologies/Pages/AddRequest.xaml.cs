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
using NewTechnologies.Services;

namespace NewTechnologies.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddRequest.xaml
    /// </summary>
    public partial class AddRequest : Page
    {
        requests _request = new requests();

        public AddRequest()
        {
            InitializeComponent();

            var context = new_technologiesEntities.GetContext();

            _request.status_request = 1;
            _request.application_date_request = DateTime.Now;
            _request.prepayment_status_request = false;

            var partners = context.partners.ToList();
            cbPartner.ItemsSource = partners;
            cbPartner.SelectedValuePath = "id_partner";
            cbPartner.DisplayMemberPath = "title_partner";

            var products = context.products.ToList();
            cbProduct.ItemsSource = products;
            cbProduct.SelectedValuePath = "id_product";
            cbProduct.DisplayMemberPath = "title_product";
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            var context = new_technologiesEntities.GetContext();

            var validator = new Validator();
            var (isRequestValid, requestErrors) = new Validator().RequestValidator(_request);

            if (!isRequestValid)
            {
                MessageBox.Show(string.Join("\n", requestErrors), "Ошибки валидации", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                context.requests.Add(_request);
                context.SaveChanges();
                MessageBox.Show("Данные сохранены!", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
