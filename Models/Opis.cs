using System;
using System.Collections.Generic;
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
        public virtual Produkt Produkt { get; set; }
        public virtual ICollection<Section> sekcje{ get; set; }
        public DateTime addDate { get; set; }

        public int adderId { get; set; }
        public virtual Person adder { get; set; }
        public DateTime? removeDate { get; set; }

        public int removeerId { get; set; }
        public virtual Person? remover { get; set; }
        public bool isDeleted { get; set; }
        public bool isVisible { get; set; }
        
    }
}