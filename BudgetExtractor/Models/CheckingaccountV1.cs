using System;
using System.Collections.Generic;

#nullable disable

namespace BudgetExtractor.Models
{
    public partial class CheckingaccountV1
    {
        public long Transid { get; set; }
        public long Accountid { get; set; }
        public long? Toaccountid { get; set; }
        public long Payeeid { get; set; }
        public string Transcode { get; set; }
        public long Transamount { get; set; }
        public string Status { get; set; }
        public string Transactionnumber { get; set; }
        public string Notes { get; set; }
        public long? Categid { get; set; }
        public long? Subcategid { get; set; }
        public string Transdate { get; set; }
        public long? Followupid { get; set; }
        public byte[] Totransamount { get; set; }
    }
}
