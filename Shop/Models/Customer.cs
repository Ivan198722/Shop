using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace Shop.Models
{
    public class Customer
    {
        public Customer()
        {

           orderDetails = new List<OrderDetail>();
        }

        [BindNever]
        public int Id { get; set; }

        [Display(Name = "Wprowadź imię")]
        [MinLength(2)]
        [Required(ErrorMessage = "Długość imienia nie może być mniejsza niż 2 znaki")]
        public string name { get; set; }

        [Display(Name = "Wprowadź nazwisko")]
        [MinLength(2)]
        [Required(ErrorMessage = "Długość nazwiska nie może być mniejsza niż 2 znaki")]
        public string surname { get; set; }

        [Display(Name = "Ulica")]
        [MinLength(3)]
        [Required(ErrorMessage = "Długość nazwy ulicy nie może być mniejsza niż 3 znaki")]
        public string street { get; set; }

        [Display(Name = "Numer domu")]
        [Required(ErrorMessage = "Wprowadź numer domu")]
        public string NumberHouse { get; set; }

        [Display(Name = "Numer lokalu")]
        public string? NumberFlat { get; set; }

        [Display(Name = "Kod pocztowy")]
        [RegularExpression(@"^\d{5}$", ErrorMessage = "Wprowadź 5 cyfr")]
        [Required(ErrorMessage = "Wprowadź kod pocztowy ")]
        [StringLength(5, MinimumLength = 5, ErrorMessage = "Wprowadź 5 cyfr")]
        public string postcode { get; set; }

        [Display(Name = "Miasto")]
        [MinLength(3)]
        [Required(ErrorMessage = "Długość nazwy miasta nie może być mniejsza niż 3 znaki")]
        public string city { get; set; }

        [Display(Name = "Numer telefonu ")]
        [DataType(DataType.PhoneNumber)]
        [MinLength(9, ErrorMessage = "Numer telefonu powinien zawierać przynajmniej 9 cyfr")]
        [Required(ErrorMessage = "Numer telefonu jest wymagany")]
        public string phone { get; set; }

        [Display(Name = "Twój adres e-mail")]
        [DataType(DataType.EmailAddress)]
        [MinLength(6, ErrorMessage = "Długość adresu e-mail nie może być mniejsza niż 6 znaków")]
        [Required(ErrorMessage = "Adres e-mail jest wymagany")]
        public string email { get; set; }

        [BindNever]
        [ScaffoldColumn(false)]
        public DateTime orderTime { get; set; }


        public IEnumerable<OrderDetail> orderDetails { get; set; }

    }
}
