using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sklep.Models.Utils
{
    public static class AppGlobalDataContext
    {
        // Klasa statyczna przechowująca dane globalne
        public static int LiczbaWyswietlen { get; set; }
    }
}