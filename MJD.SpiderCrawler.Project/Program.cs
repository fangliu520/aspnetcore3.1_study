using MJD.SpiderCrawler.Project.Model;
using System;

namespace MJD.SpiderCrawler.Project
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            TencentCategoryEntity category = new TencentCategoryEntity()
            {
                Url = "https://ke.qq.com/course/list/%E6%9E%B6%E6%9E%84%E5%B8%88?page=1"
            };
            ISearch search = new CourseSearch(category);
            search.Crawler();

            Console.ReadKey();

        }
    }
}
