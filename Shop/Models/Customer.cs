using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace Shop.Models
{
    public class Customer
    {
        [BindNever]
        public int Id { get; set; }

        //[Display(Name = "Imię")]
        //[MinLength(2)]
        //[Required(ErrorMessage = "Pole jest wymagane")]
        public string name { get; set; }

        //[Display(Name = "Nazwisko")]
        //[MinLength(2)]
        //[Required(ErrorMessage = "Pole jest wymagane")]
        public string surname { get; set; }

        //[Display(Name = "ulica")]
        //[MinLength(3)]
        //[Required(ErrorMessage = "Pole jest wymagane")]
        public string street { get; set; }

        //[Display(Name = "nr domu")]
        //[Required(ErrorMessage = "*")]
        public string NumberHouse { get; set; }

       // [Display(Name = "nr lokalu")]
        public string? NumberFlat { get; set; }

        //[Display(Name = "Kod pocztowy")]
        //[RegularExpression(@"^\d{5}$", ErrorMessage = "Wprowadź 5 cyfr")]
        //[Required(ErrorMessage = "__-___")]
        //[StringLength(6, MinimumLength = 6, ErrorMessage = "Wprowadź 5 cyfr")]
        public string postcode { get; set; }

        //[Display(Name = "miejscowość")]
        //[MinLength(3)]
        //[Required(ErrorMessage = "Pole jest wymagane")]
        public string city { get; set; }

        //[Display(Name = "nr telefonu ")]
        //[DataType(DataType.PhoneNumber)]
        //[MinLength(9, ErrorMessage = "Numer telefonu powinien zawierać przynajmniej 9 cyfr")]
        //[Required(ErrorMessage = "Numer telefonu jest wymagany")]
        public string phone { get; set; }

        //[Display(Name = "adres e-mail")]
        //[DataType(DataType.EmailAddress)]
        //[MinLength(6, ErrorMessage = "Długość adresu e-mail nie może być mniejsza niż 6 znaków")]
        //[Required(ErrorMessage = "Adres e-mail jest wymagany")]
        public string email { get; set; }

       public virtual Order Order { get; set; }
    }
}
