using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Razor.Parser;

namespace Sklep.Models
{
    /**
     * @author Artur Leszczak 
     * @desc PersonRegister odpowiada modelowi rejestracji nowego użytkownika
     */
    public class Person
    {
        [Key]
        public int PersonId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public DateTime Birthday { get; set; }
        public int LogowanieId { get; set; }
        public int AccountTypeId { get; set; }
        public Logowanie Logowanie { get; internal set; }
    }
}