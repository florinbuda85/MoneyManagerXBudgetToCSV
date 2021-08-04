using System;
using System.Collections.Generic;
using System.Text;

namespace BudgetExtractor.Models
{
    public class ReportLine
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public int IdCategory { get; set; }
        public string NameCategory { get; set; }
        public double Spent { get; set; }
        public double Allocated { get; set; }
        public double Left { 
            get {
                return Allocated - Spent;
            }
        }
    }
}
