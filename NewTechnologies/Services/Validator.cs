using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewTechnologies.Model;

namespace NewTechnologies.Services
{
    internal class Validator
    {
        public (bool isValid, List<string> errors) PartnerValidate(partners partner)
        {
            var errors = new List<string>();

            if (string.IsNullOrEmpty(partner.title_partner) || partner.title_partner.Length < 2 || partner.title_partner.Length > 50)
                errors.Add("Название должно содержать от 2 до 50 символов");

            if (partner.type_partner == 0)
                errors.Add("Выберите тип");

            if (string.IsNullOrEmpty(partner.director_partner) || partner.director_partner.Length < 2 || partner.director_partner.Length > 80)
                errors.Add("ФМО директора должно содержать от 3 до 80 символов");

            if (string.IsNullOrEmpty(partner.email_partner) || partner.email_partner.Length < 2 || partner.email_partner.Length > 60)
                errors.Add("Email должен содержать от 2 до 60 символов");

            if (string.IsNullOrEmpty(partner.phone_partner) || partner.phone_partner.Length < 2 || partner.phone_partner.Length > 15)
                errors.Add("Номер телефона должен содержать от 2 до 15 символов");

            if (string.IsNullOrEmpty(partner.legal_address_partner) || partner.legal_address_partner.Length < 2 || partner.legal_address_partner.Length > 200)
                errors.Add("Юридический адрес должен содержать от 2 до 200 символов");

            if (partner.inn_partner <= 0 || partner.inn_partner > 9999999999)
                errors.Add("ИНН должен быть не более 10 цифр");

            if (partner.rating_partner != 0 && (partner.rating_partner < 0 || partner.rating_partner > 10))
                errors.Add("Рейтинг должен быть от 0 до 10");

            return (errors.Count == 0, errors);
        }

        public (bool isValid, List<string> errors) RequestValidator(requests request)
        {
            var errors = new List<string>();

            if (request.partner_request <= 0)
                errors.Add("Выберите партнера");

            if (request.product_request <= 0)
                errors.Add("Выберите продукт");

            if (request.quantity_request <= 0)
                errors.Add("Укажите количество продукции");

            return (errors.Count == 0, errors);
        }
    }
}
