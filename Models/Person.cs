using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Razor.Parser;

namespace SklepInternetowy.Models
{
    /**
         * @author Artur Leszczak 
         * @desc PersonRegister odpowiada definicji użytkownika serwisu
         */
    public class Person
    {
        public int Id { get; set; }
        [Required]
        [DisplayName("Imie")]
        public string Name { get; set; }
        [Required]
        [DisplayName("Nazwisko")]
        public string Surname { get; set; }
        [Required]
        [DisplayName("Email")]
        public string Email { get; set; }
    }

        /**
         * @author Artur Leszczak 
         * @desc PersonRegister odpowiada modelowi rejestracji nowego użytkownika , dziedziczy od klasy Person
         */
        public class PersonRegister : Person
    {
       
        [Required]
        [DisplayName("Potwierdź email")]
        public string ConfirmEmail { get; set; }
        [Required]
        [DisplayName("Hasło")]
        public string Password { get; set; }
        [Required]
        [DisplayName("Potwierdź hasło")]
        public string ConfirmPassword { get; set; }
    }

    /**
     * @author Artur Leszczak 
     * @desc PersonLogin odpowiada modelowi potrzebnemu do przeprowadzenia logowania użytkownika do systemu
     */
    public class PersonLogin
    {
        [Required]
        [DisplayName("Wprowadź adres email")]
        public string Email { get; set; }
        [Required]
        [DisplayName("Wprowadź hasło")]
        public string Password { get; set; }

    }
}