using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOTC2024.Day5
{
    public class Page
    {
        public Page(int  id)
        {
            Id = id;
            PrintBefore = new List<Page>();
        }

        public int Id { get; set; }

        public List<Page> PrintBefore { get; set; }

        public void AddOrderingRule(Page page)
        {
            if(!PrintBefore.Where(x => x.Id == page.Id).Any()) 
                PrintBefore.Add(page); 
        }
    }
}
