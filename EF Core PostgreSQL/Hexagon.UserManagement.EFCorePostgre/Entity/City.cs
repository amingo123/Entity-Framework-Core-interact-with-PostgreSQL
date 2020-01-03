using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Hexagon.UserManagement.EFCorePostgre.Entity
{
    public class City
    {
        [Key]
        public string Id { get; set; }

        public string CityName { get; set; }
    }
}
