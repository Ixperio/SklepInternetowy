using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Sklep.Models
{
    public class Parametr
    {
        public int Parametrid {  get; set; }
        public string name { get; set; }

        public string jednostka { get; set; }

        public int typ_JednostkiId { get; set; }

        public int PersonAdderId { get; set; }

        public DateTime addDate { get; set; }

        public int? PersonRemoverId { get; set; }
        public DateTime? removeDate { get; set; }

        public bool isDeleted { get; set; }

        public bool isVisible { get; set; }

    }
}