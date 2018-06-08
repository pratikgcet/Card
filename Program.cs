using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Xml.Linq;
using System.Xml.Serialization;
using static System.String;

namespace DataTest.Services
{
    public class Program
    {
        public static void Main()
        {
            TestJoin();
            var list = new List<Stock>
            {
                new Stock {Symbol = "A", Price = 1, Time = DateTime.Today, Source = "Bob"},
                new Stock {Symbol = "A", Price = 2, Time = DateTime.Today.AddDays(1), Source = "Bob"},
                new Stock {Symbol = "A", Price = 2, Time = DateTime.Today.AddDays(3), Source = "Bob"},
                new Stock {Symbol = "A", Price = 3, Time = DateTime.Today.AddDays(3), Source = "Rob"},
                new Stock {Symbol = "A", Price = 1, Time = DateTime.Today.AddDays(4), Source = "Bob"},
                new Stock {Symbol = "A", Price = 1, Time = DateTime.Today.AddDays(5), Source = "Bob"},
                new Stock {Symbol = "B", Price = 1, Time = DateTime.Today.AddDays(3), Source = "Bob"},
            };
            var sources = new List<string>() {"Bob", "Rob"};
            var listofStock = list.Select(a => a.Symbol).Distinct();
            var listofPrice=new List<Stock>();
            var numberofDays = (DateTime.Today.AddDays(3) - DateTime.Today).Days;
            foreach (var stock in listofStock)
            {
                bool mainDataSource = true;
                foreach (var source in sources)
                {
                    
                    if (mainDataSource)
                    {
                        var res = list.Where(x =>
                                x.Time >= DateTime.Today && x.Time <= DateTime.Today.AddDays(3) && x.Source == source && x.Symbol==stock)
                            .ToList();
                        listofPrice.AddRange(res);
                        mainDataSource = false;
                        continue;
                    }
                    if (listofPrice.Count(a => a.Symbol==stock) < numberofDays)
                    {
                        var dates= Enumerable.Range(0, (DateTime.Today.AddDays(3) - DateTime.Today).Days + 1).Select(d => DateTime.Today.AddDays(d));
                        
                        var datesMissing = dates
                                            .Where(p => listofPrice.All(p2 => (p2.Time.Date != p.Date) && p2.Symbol==stock));

                        var res = list.Where(x =>x.Source == source &&x.Symbol==stock)
                            .Where(p => datesMissing.Any(l => p.Time == l.Date))
                            .ToList();
                        listofPrice.AddRange(res);
                    }

                   
                }

            }
            var prop = typeof(Stock).GetProperty("Price");
            var orderedList = listofPrice.OrderBy(a => prop?.GetValue(a, null));



        }

        private static void TestJoin()
        {
            var list = new List<Stock>
            {
                new Stock {Symbol = "A", Price = 1, Time = DateTime.Today, Source = "Bob"},
                new Stock {Symbol = "A", Price = 2, Time = DateTime.Today.AddDays(1), Source = "Bob"},
                new Stock {Symbol = "A", Price = 2, Time = DateTime.Today.AddDays(3), Source = "Bob"},
                new Stock {Symbol = "A", Price = 3, Time = DateTime.Today.AddDays(3), Source = "Rob"},
                new Stock {Symbol = "A", Price = 1, Time = DateTime.Today.AddDays(4), Source = "Bob"},
                new Stock {Symbol = "A", Price = 1, Time = DateTime.Today.AddDays(5), Source = "Bob"},
                new Stock {Symbol = "B", Price = 1, Time = DateTime.Today.AddDays(3), Source = "Bob"},
            };

            var res = list.GroupBy(a => a.Symbol, a => new {a.Time, a.Price, a.Source},
                (a, b) => new {Symbol = a, Prices = b.ToList().OrderBy(x => x.Source).FirstOrDefault()}).ToList();

        }

        public class SourceOrder
        {
            public string Source { get; set; }
            public int Order { get; set; }     
        }

        
    }

    
}
