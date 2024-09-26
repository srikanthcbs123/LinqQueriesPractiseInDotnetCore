using System;
using System.Collections.Generic;

namespace LinqQueriesPractiseInDotnetCore.DBCONNECT
{
    public partial class ProductsAboveAveragePrice
    {
        public string ProductName { get; set; } = null!;
        public decimal? UnitPrice { get; set; }
    }
}
