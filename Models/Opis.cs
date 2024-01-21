using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Sklep.Models
{
    /**
     * @brief Opis produktu w sklepie
     * 
     * @author Artur Leszczak
     */
    public class Opis
    {
        public int OpisId { get; set; }
        public int ProduktId { get; set; }

        public DateTime addDate { get; set; }

        public int adderId { get; set; }
        public DateTime? removeDate { get; set; }

        public int? removerId { get; set; }
        public bool isDeleted { get; set; }
        public bool isVisible { get; set; }
        
    }
}