using System;
using System.Collections.Generic;

namespace LinqQueriesPractiseInDotnetCore.Northwind
{
    public partial class Territory
    {
        public Territory()
        {
            Employees = new HashSet<Employee1>();
        }

        public string TerritoryId { get; set; } = null!;
        public string TerritoryDescription { get; set; } = null!;
        public int RegionId { get; set; }

        public virtual Region Region { get; set; } = null!;

        public virtual ICollection<Employee1> Employees { get; set; }
    }
}
