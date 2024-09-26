using System;
using System.Collections.Generic;

namespace LinqQueriesPractiseInDotnetCore.Northwind
{
    public partial class Department
    {
        public Department()
        {
            Employee1s = new HashSet<Employee1>();
        }

        public long Id { get; set; }
        public string? DeptName { get; set; }

        public virtual ICollection<Employee1> Employee1s { get; set; }
    }
}
