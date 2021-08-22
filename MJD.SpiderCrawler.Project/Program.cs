using MJD.SpiderCrawler.Project.Model;
using System;

namespace MJD.SpiderCrawler.Project
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            #region 抓取腾讯课堂搜索架构师关键字的课程信息
            //TencentCategoryEntity category = new TencentCategoryEntity()
            //{
            //    Url = "https://ke.qq.com/course/list/%E6%9E%B6%E6%9E%84%E5%B8%88?page=1"
            //};
            //ISearch search = new CourseSearch(category);
            //search.Crawler();
            #endregion
            #region 抓取腾讯课堂所有类目
            //TencentCategoryEntity category = new TencentCategoryEntity()
            //{
            //    Url = "https://ke.qq.com/course/list"
            //};
            //ISearch search = new CourseSearch(category);
            //search.CrawlerCategory();
            #endregion


            #region 抓取腾讯课堂所有类目
            TencentCategoryEntity category = new TencentCategoryEntity()
            {
                Url = "https://ke.qq.com/course/list"
            };
            ISearch search = new CourseSearch(category);
            CrawlerAllCourse.GetAllCourse(search.CrawlerCategory());
            #endregion

            Console.ReadKey();

        }
    }
}
