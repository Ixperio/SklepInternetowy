using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Sklep.Models
{
    public class Photo
    {
       public int PhotoId { get; set; }
       public string link { get; set; }
       public int positionX { get; set; }
       public int positionY { get; set; }
       public int sizeX { get; set; }
       public int sizeY { get; set; }
       public bool isDeleted { get; set; }
       public bool isVisible { get; set; }
       public int adderId { get; set; }
       public int SectionId { get; set; }
       public int ProductId { get; set; }

    } 
}