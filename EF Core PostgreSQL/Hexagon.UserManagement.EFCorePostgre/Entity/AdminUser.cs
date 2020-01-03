using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Hexagon.UserManagement.EFCorePostgre.Entity
{
    public class AdminUser
    {
        public int Id { get; set; }
        [MaxLength(255)]
        public string Name { get; set; }

        //public List<Role> Roles { get; set; }

        public int? Age { get; set; }

        public DateTime? TokenExpireTime { get; set; }

        public int? RealmId { get; set; }

        public Realm Realm { get; set; }

        public string Gender { get; set; }
    }
}
