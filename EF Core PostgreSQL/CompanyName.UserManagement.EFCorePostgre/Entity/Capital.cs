using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CompanyName.UserManagement.EFCorePostgre.Entity
{
    public class Capital
    {
        [ForeignKey(nameof(City))]
        public string Id { get; set; }
        public string other { get; set; }

        public City City { get; set; }
    }
}
