using System;
using System.Collections.Generic;
using System.Text;

namespace Hexagon.UserManagement.EFCorePostgre.Entity
{
    public class RealmSolution
    {
        public int RlmId { get; set; }

        public int SlnId { get; set; }

        public Realm Realm { get; set; }

        public Solution Solution { get; set; }

    }
}
