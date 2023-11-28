using Antlr.Runtime.Tree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sklep.Models
{
    /**
     * @brief Klasa opisująca Kategroię sprzedażowe w sklepie
     * 
     * @author Artur Leszczak
     */
    public class Kategoria
    {
        public int KategoriaId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int PersonAdderId { get; set; }
        public virtual Person PersonAdder { get; set; } //adder

        public int? PersonRemoverId { get; set; }
        public virtual Person PersonRemover { get; set; } //remover 
        public bool isDeleted { get; set; }
        public bool isVisible { get; set; }

        public int RodzajId { get; set; }
        public virtual Rodzaj Rodzaj { get; set; }
    }
    
}