using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOTC2024.Day5
{
    public class Update
    {
        public Update(List<Page> pagesToPrint)
        {
            PagesToPrint = pagesToPrint.ToList();
        }

        public List<Page> PagesToPrint { get; set; }

        public List<Page> PagesPrinted { get; set; } = new List<Page>();

        public int MiddlePageNumber
        {
            get
            {
                return PagesToPrint[PagesToPrint.Count / 2].Id;
            }
        }

        public bool IsCorrect()
        {
            foreach(var page in PagesToPrint)
            {
                foreach(var pageRule in page.PrintBefore)
                {
                    if(PagesPrinted.Where(x => x.Id == pageRule.Id).Any())
                    {
                        return false;
                    }
                }

                PagesPrinted.Add(page);
            }

            return true;
        }

        public int CorrectPageOrder()
        {
            // Already correct so shouldn't have been called
            if (IsCorrect())
                return 0;
            PagesPrinted = new List<Page>();
            while (!IsCorrect())
            {
                PagesPrinted = new List<Page>();
                var madeSwap = false;
                
                foreach (var page in PagesToPrint)
                {                    
                    foreach (var pageRule in page.PrintBefore)
                    {                        
                        if (PagesPrinted.Where(x => x.Id == pageRule.Id).Any())
                        {
                            var indexA = PagesToPrint.FindIndex(x => x.Id == page.Id);
                            var indexB = PagesToPrint.FindIndex(x => x.Id == pageRule.Id);
                            (PagesToPrint[indexA], PagesToPrint[indexB]) = (PagesToPrint[indexB], PagesToPrint[indexA]);
                            madeSwap = true;
                            PagesPrinted = new List<Page>();                           
                        }

                        if (madeSwap) break;
                    }

                    if (madeSwap) break;
                    PagesPrinted.Add(page);
                }
                PagesPrinted = new List<Page>();
            }

            return MiddlePageNumber;
        }
    }
}
