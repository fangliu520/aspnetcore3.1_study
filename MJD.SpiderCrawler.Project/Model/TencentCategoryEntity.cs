using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MJD.SpiderCrawler.Project.Model
{
    public class TencentCategoryEntity : BaseModel
    {
        public string code { get; set; }
        public string Code { get; set; }
        public string ParentCode { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string CategoryLevel { get; set; }

        public string State { get; set; }
    }
}
