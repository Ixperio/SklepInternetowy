using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;

namespace Sklep.Models
{
    public class ConfigData
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        
        public string? Value { get; set; }

        public string? Description { get; set; }
        
        public bool isDeleted { get; set; } = false;
    }
}