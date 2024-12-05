using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOTC2024.Day5
{
    public class Printer
    {
        public Printer(List<string> lines)
        {
            Pages = new List<Page>();
            Updates = new List<Update>();
            ParseInput(lines);
        }

        public List<Page> Pages { get; set; }

        public List<Update> Updates { get; set; }

        public int Calc()
        {
            var total = 0;

            foreach(var update in Updates)
            {
                if (update.IsCorrect())
                    total += update.MiddlePageNumber;
            }

            return total;
        }       

        public int CalcIncorrectPages()
        {
            var total = 0;

            foreach (var update in Updates)
            {
                if (!update.IsCorrect())
                {
                    total += update.CorrectPageOrder();
                }
            }

            return total;
        }

        private void ParseInput(List<string> lines)
        {
            foreach(var line in lines)
            {
                if(line.Contains("|"))
                {
                    var ln = line.Trim().Split('|');

                    var page1 = Pages.FirstOrDefault(x => x.Id == int.Parse(ln[0]));                    
                    if (page1 is null)
                    {
                        page1 = new Page(int.Parse(ln[0]));
                        Pages.Add(page1);
                    }

                    var page2 = Pages.FirstOrDefault(x => x.Id == int.Parse(ln[1]));
                    if (page2 is null)
                    {
                        page2 = new Page(int.Parse(ln[1]));
                        Pages.Add(page2);
                    }

                    page1.AddOrderingRule(page2);
                }
                else if(line.Contains(",")) 
                {
                    var ln = line.Trim().Split(",");
                    var pagesToPrint = new List<Page>();
                    foreach(var pageId in ln)
                    {
                        var id = int.Parse(pageId);
                        var page = Pages.FirstOrDefault(x => x.Id == id);
                        if(page is null)
                        {
                            page = new Page(id);
                            Pages.Add(page);
                        }
                        pagesToPrint.Add(page);
                    }
                    Updates.Add(new Update(pagesToPrint));
                }
            }
        }
    }
}
