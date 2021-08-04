using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BudgetExtractor.Models;
using Microsoft.EntityFrameworkCore.Design;
using Newtonsoft.Json;

namespace BudgetExtractor
{
    class Program
    {

        private static int budgetId = 0;
        private static Settings settings;
        static void Main(string[] args)
        {

            settings = JsonConvert.DeserializeObject<Settings>(File.ReadAllText("settings.json"));

            var context = new Models.NewContext(settings.DatabasePath);

            var categories = context.CategoryV1s.ToList();

            string dateFilter = DateTime.Now.ToString("yyyy-MM-");

            var payments = context.CheckingaccountV1s.Where(x => x.Transdate.Contains(dateFilter)).ToList();

            List<ReportLine> lines = new List<ReportLine>();

            string content = "";

            categories.ForEach(c =>
            {
                lines.Add(new ReportLine()
                {
                    IdCategory = (int)c.Categid,
                    NameCategory = c.Categname,
                    Spent = payments.Where(p => p.Categid == c.Categid).Sum(x => x.Transamount),
                    Allocated = GetAllocatedSum(context, (int)c.Categid),
                    Month = DateTime.Now.Month,
                    Year = DateTime.Now.Year
                });
            });

            content += $"Category,Alocated,Spent,Left";

            lines.OrderBy(x=>x.NameCategory).ToList().ForEach(x =>
            {
                content += $"\n{x.NameCategory},{x.Allocated},{x.Spent},{x.Left}";
            });

            File.WriteAllText($@"{settings.ExportFolder}\result_{DateTime.Now.ToString("yyyy_MM")}.csv", content);
        }

        public static double GetAllocatedSum(Models.NewContext newContext, int catId)
        {
            if (budgetId == 0)
            {
                budgetId = GetBudgetYearId(newContext);
            }

            try
            {
                return -1 * newContext.BudgettableV1s
                            .Where(x => x.Budgetyearid == budgetId && x.Categid == catId).First().Amount;
            }
            catch (Exception)
            {
                Console.WriteLine($"No amount for id {catId}");
                return 0;
            }
        }

        public static int GetBudgetYearId(Models.NewContext newContext)
        {
            string dateFilter = DateTime.Now.ToString("yyyy-MM");
            return (int)newContext.BudgetyearV1s.Where(x => x.Budgetyearname.Contains(dateFilter)).FirstOrDefault().Budgetyearid;
        }
    }
}
