using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sklep.Models
{
    /**
     * @brief Odpowiada za typ konta
     */
    public class AccountType
    {
        public int AccountTypeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool isDeleted { get; set; } = false;

        public virtual ICollection<Person> Person { get; set; }

    }
}