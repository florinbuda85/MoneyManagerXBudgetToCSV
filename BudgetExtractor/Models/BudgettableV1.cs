using System;
using System.Collections.Generic;

#nullable disable

namespace BudgetExtractor.Models
{
    public partial class BudgettableV1
    {
        public long Budgetentryid { get; set; }
        public long? Budgetyearid { get; set; }
        public long? Categid { get; set; }
        public long? Subcategid { get; set; }
        public string Period { get; set; }
        public double Amount { get; set; }
    }
}
