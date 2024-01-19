using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sklep.Models
{
    public class Section
    {
        public int SectionId { get; set; }
        public string Title { get; set; }
        public virtual ICollection<Photo>? photos { get; set; }
        public string Description { get; set; } //html editor

        public int adderId { get; set; }
        public virtual Person adder { get; set; }

        public DateTime addDate { get; set; }

        public int removerId { get; set; }
        public virtual Person? remover { get; set; }

        public DateTime? removerDate { get; set;}

        public bool isDeleted { get; set; }
        public bool isVisible { get; set; }

        public int OpisId { get; set; }
        public virtual Opis Opis { get; set; }

    }
}