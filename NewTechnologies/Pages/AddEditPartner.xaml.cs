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
    /// Логика взаимодействия для AddEditPartner.xaml
    /// </summary>
    public partial class AddEditPartner : Page
    {
        partners _partner = new partners();

        public AddEditPartner(partners partner)
        {
            InitializeComponent();

            if (partner != null)
            {
                _partner = partner;
                btnDelete.Visibility = Visibility.Visible;
            }

            var context = new_technologiesEntities.GetContext();

            var types = context.partner_type.ToList();
            cbType.ItemsSource = types;
            cbType.SelectedValuePath = "id_partner_type";
            cbType.DisplayMemberPath = "title_partner_type";

            DataContext = _partner;

        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            var context = new_technologiesEntities.GetContext();

            var validator = new Validator();
            var (isPartnerValid, partnerErrors) = new Validator().PartnerValidate(_partner);

            if (!isPartnerValid)
            {
                MessageBox.Show(string.Join("\n", partnerErrors), "Ошибки валидации", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (_partner.id_partner == 0)
            {
                context.partners.Add(_partner);
                NavigationService.GoBack();
            }
            else
            {
                var partnerInDb = context.partners.FirstOrDefault(p => p.id_partner == _partner.id_partner);

                if (partnerInDb != null)
                {
                    partnerInDb.title_partner = _partner.title_partner;
                    partnerInDb.type_partner = _partner.type_partner;
                    partnerInDb.director_partner = _partner.director_partner;
                    partnerInDb.email_partner = _partner.email_partner;
                    partnerInDb.phone_partner = _partner.phone_partner;
                    partnerInDb.legal_address_partner = _partner.legal_address_partner;
                    partnerInDb.inn_partner = _partner.inn_partner;
                    partnerInDb.rating_partner = _partner.rating_partner;
                }
                else
                {
                    MessageBox.Show("Партнер не найден для обновления", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            try
            {
                context.SaveChanges();
                MessageBox.Show("Данные сохранены!", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var context = new_technologiesEntities.GetContext();

            try
            {
                if (MessageBox.Show($"Вы действительно хотите удалить партнера {_partner.title_partner}?", "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    var partnerToDelete = context.partners.FirstOrDefault(p => p.id_partner == _partner.id_partner);

                    if (partnerToDelete == null)
                    {
                        MessageBox.Show("Партнер не найден в базе данных", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    context.partners.Remove(partnerToDelete);
                    context.SaveChanges();
                    MessageBox.Show("Партнер успешно удален", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    NavigationService.GoBack();
                }
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException dbEx)
            {
                MessageBox.Show($"Ошибка при удалении: Возможно, партнер связан с другими записями.\n{dbEx.InnerException?.Message}", "Ошибка базы данных", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при удалении: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
