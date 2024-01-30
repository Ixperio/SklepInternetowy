using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sklep.Models.ModelViews
{
    public class OrderView
    {

        public string Imie { get; set; }

        public string Nazwisko { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Street { get; set; }

        public string NumerDomu { get; set; }

        public string Post { get; set; }

        public string Town { get; set; }

        public int DostawaId { get; set; }

        public int PlatnoscId { get; set; }

    }
}