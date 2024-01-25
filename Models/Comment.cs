using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sklep.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public string Description { get; set; }
        public int ProductId { get; set; }
        public DateTime Date { get; set; }
        public bool IsDeleted { get; set; }

    }
}