using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using BudgetExtractor.Models;
using Microsoft.EntityFrameworkCore.Design;
using Newtonsoft.Json;

namespace BudgetExtractor
{
    class Program
    {

        static double totalIn = 0;
        static double totalOut = 0;
        


        private static int budgetId = 0;
        private static Settings settings;
        static void Main(string[] args)
        {
            var categorySpendingRows = new Dictionary<long, string>();

            settings = JsonConvert.DeserializeObject<Settings>(File.ReadAllText("settings.json"));
            string dateFilter = DateTime.Now.ToString("yyyy-MM-");

            var context = new Models.NewContext(settings.DatabasePath);
            var ignoredCategories = settings.IgnoreCategories.Split(",").ToList();

            var incomingCategId = context.CategoryV1s
                                            .Where(x => x.Categname == "Venituri")
                                            .ToList().First().Categid;

            totalIn = context.CheckingaccountV1s
                                            .Where(x => x.Transdate.Contains(dateFilter) && x.Categid == incomingCategId)
                                            .Sum(x => x.Transamount);

            var categoriesOut = context.CategoryV1s
                                            .Where(x => !ignoredCategories.Contains(x.Categname))
                                            .ToList();

            var payments = context.CheckingaccountV1s
                                            .Where(x => x.Transdate.Contains(dateFilter))
                                            .ToList();

            List<ReportLine> lines = new List<ReportLine>();

            categoriesOut.ForEach(c =>
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

                var tmp = "";

                payments.Where(p => p.Categid == c.Categid).ToList()
                    .ForEach(x => {
                        tmp += $"<tr><td>{x.Transdate}</td><td>{AsText(x.Transamount)}</td><td>{GetAcountName(context, x.Accountid)}</td><td>{x.Notes}</td></tr>";

                        totalOut += x.Transamount;
                    });

                categorySpendingRows.Add(c.Categid, tmp); 

            });


            //750  la 2.30

            var htmlMain = File.ReadAllText("HTML/Main.html");

            var hideSubcat = "";
            var rows = "";



            lines.OrderBy(x=>x.NameCategory).ToList().ForEach(x =>
            {
                hideSubcat += $"\n$(\".detail_{x.IdCategory}\").hide();";

                rows += File.ReadAllText("HTML/row.html")
                                .Replace("{id}", x.IdCategory.ToString())
                                .Replace("{name}", x.NameCategory)
                                .Replace("{spent}", AsText(x.Spent))
                                .Replace("{allocated}", AsText(x.Allocated))
                                .Replace("{left}", AsText(x.Left))
                                .Replace("{status}", x.Left <= 0 ? "danger":"light")
                                .Replace("<!--category_list_of_spendings-->", categorySpendingRows[x.IdCategory]);
            });

            htmlMain = htmlMain.Replace("//hide_all", hideSubcat);
            htmlMain = htmlMain.Replace("<!-- rows -->", rows);
            htmlMain = htmlMain.Replace("<!-- IN -->", AsText(totalIn));
            htmlMain = htmlMain.Replace("<!-- OUT -->", AsText(totalOut));


            htmlMain += "Generat la " + DateTime.Now.ToString();

            File.WriteAllText($@"{settings.ExportFolder}\Buget_{DateTime.Now.ToString("yyyy_MM")}.html", htmlMain);
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

        public static string AsText (double d)
        {
            return d.ToString("c", CultureInfo.CreateSpecificCulture("ro-RO"));
        }

        public static int GetBudgetYearId(Models.NewContext newContext)
        {
            string dateFilter = DateTime.Now.ToString("yyyy-MM");
            return (int)newContext.BudgetyearV1s.Where(x => x.Budgetyearname.Contains(dateFilter)).FirstOrDefault().Budgetyearid;
        }

        public static string GetAcountName(Models.NewContext newContext, long accId)
        {
            return newContext.AccountlistV1s.Where(x => x.Accountid == accId).FirstOrDefault().Accountname;
        }

    }
}
