using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Sklep.Models.ModelViews
{
    //Klasa widoku odpowiedzialna za model widoku formularza rejestacji nowych użytkowników wraz z ograniczeniami nałożonymi na
    //konkretne typy - Artur Leszczak i Katarzyna Grygo
    public class PersonRegistration
    {
        [Required]
        [MaxLength(32)]
        [MinLength(5)]
        [DisplayName("Podaj login")]
        public string Login { get; set; }
        [Required]
        [MaxLength(32)]
        [MinLength(8)]
        [DataType(DataType.Password)]
        [DisplayName("Hasło")]
        public string Password { get; set; }
        [Required]
        [MaxLength(32)]
        [MinLength(8)]
        [DataType(DataType.Password)]
        [DisplayName("Potwierdź hasło")]
        public string PasswordConfirm { get; set; }
        [Required]
        [EmailAddress]
        [DisplayName("Podaj email")]
        public string Email { get; set; }
        [Required]
        [EmailAddress]
        [DisplayName("Potwierdź email")]      
        public string EmailConfirm { get; set; }
        [Required]
        [MaxLength(32)]
        [DisplayName("Imie")]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(32)]
        [DisplayName("Nazwisko")]
        public string LastName { get; set; }
        [Required]
        [MaxLength(9)]
        [MinLength(9)]
        [DisplayName("Numer telefonu")]
        public string PhoneNumber { get; set; }
        [Required]
        [DisplayName("Data urodzenia")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
    }
}
