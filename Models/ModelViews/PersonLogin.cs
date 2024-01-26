using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Sklep.Models.ModelViews
{
    //Klasa zawierajaca model przekazywanych danych do logowania 
    public class PersonLogin
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
        [DisplayName("Podaj hasło")]
        public string Password { get; set; }
    }
}