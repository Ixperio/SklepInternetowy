using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sklep.Models
{
    public class Komentarz
    {
        [Key]
        public int CommentId { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Content { get; set; }

        public DateTime CreatedAt { get; set; }

        [ForeignKey("Produkt")]
        public int ProduktId { get; set; }
        [ForeignKey("ProduktId")]
        public virtual Produkt Produkt { get; set; }
    }
}
