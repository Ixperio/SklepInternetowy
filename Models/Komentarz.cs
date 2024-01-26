using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sklep.Models
{
    public class Komentarz
    {
        [Key]
        public int CommentId { get; set; }

        public int UserId { get; set; }

        [Required]
        public string Content { get; set; }

        public DateTime CreatedAt { get; set; }

        public int ProduktId { get; set; }

        public bool isDeleted { get; set; }

    }
}
