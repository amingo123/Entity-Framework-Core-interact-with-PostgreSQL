using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Hexagon.UserManagement.EFCorePostgre.Entity
{
    public class Solution
    {
        public Solution()
        {
            RealmSolutions = new List<RealmSolution>();
        }
        public int SlnId { get; set; }

        public string SolutionName { get; set; }

        public virtual ICollection<RealmSolution> RealmSolutions { get; set; }
    }
}
