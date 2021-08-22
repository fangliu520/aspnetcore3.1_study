﻿using MJD.SpiderCrawler.Project.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MJD.SpiderCrawler.Project
{
    public interface ISearch
    {
        void Crawler();
        void CrawlerAll();
        List<TencentCategoryEntity> CrawlerCategory();     
    }
}
