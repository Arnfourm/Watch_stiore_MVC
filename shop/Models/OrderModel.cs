using System.ComponentModel.DataAnnotations;

namespace OrderModel.Models
{
    public class Order
    {
        public int IdOrder { get; set; }

        [Required(ErrorMessage = "Имя обязательно!")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Не менее 2 и не более 50 символов")]
        public string NameOrder { get; set; }

        [Required(ErrorMessage = "Фамилия обязательна!")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Не менее 2 и не более 50 символов")]
        public string SecondNameOrder { get; set; }

        [Required(ErrorMessage = "Почтовый ящик обязателен!")]
        [EmailAddress(ErrorMessage = "Некорректый адрес")]
        public string EmailOrder { get; set; }

        [Required(ErrorMessage = "Адрес обязателен!")]
        public string AddressOrder { get; set; }

        [Required(ErrorMessage = "Количество обязательно!")]
        [Range(1, 10, ErrorMessage = "Недопустимое количество")]
        public int QuantityOrder { get; set; }
    }
}
