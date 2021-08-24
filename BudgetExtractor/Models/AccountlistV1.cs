using System;
using System.Collections.Generic;

#nullable disable

namespace BudgetExtractor.Models
{
    public partial class AccountlistV1
    {
        public long Accountid { get; set; }
        public string Accountname { get; set; }
        public string Accounttype { get; set; }
        public string Accountnum { get; set; }
        public string Status { get; set; }
        public string Notes { get; set; }
        public string Heldat { get; set; }
        public string Website { get; set; }
        public string Contactinfo { get; set; }
        public string Accessinfo { get; set; }
        public byte[] Initialbal { get; set; }
        public string Favoriteacct { get; set; }
        public long Currencyid { get; set; }
        public long? Statementlocked { get; set; }
        public string Statementdate { get; set; }
        public byte[] Minimumbalance { get; set; }
        public byte[] Creditlimit { get; set; }
        public byte[] Interestrate { get; set; }
        public string Paymentduedate { get; set; }
        public byte[] Minimumpayment { get; set; }
    }
}
