using System;
using System.Collections.Generic;
using System.Text;

namespace Hexagon.UserManagement.EFCorePostgre.Entity
{
    public class Realm
    {
        public int RlmId { get; set; }

        public string RealmName { get; set; }

        public virtual ICollection<AdminUser> Users { get; set; }

        public virtual ICollection<RealmSolution> RealmSolutions { get; set; }
    }
}
