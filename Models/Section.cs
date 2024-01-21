using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Sklep.Models
{
    public class Section
    {
        public int SectionId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; } //html editor
        public int adderId { get; set; }
        public DateTime addDate { get; set; }
        public int? removerId { get; set; }
        public DateTime? removerDate { get; set;}
        public bool isDeleted { get; set; }
        public bool isVisible { get; set; }
        public int OpisId { get; set; }

    }
}