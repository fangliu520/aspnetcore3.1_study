using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MJD.SpiderCrawler.Project.Model
{
   public class CourseEntity
    {
        public long CourseId { get; set; }

        public int CategoryId { get; set; }

        public string Title { get; set; }

        public Decimal Price { get; set; }

        public string Url { get; set; }

        public string ImageUrl { get; set; }
    }
}
