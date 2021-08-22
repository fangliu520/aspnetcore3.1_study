using MJD.SpiderCrawler.Project.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MJD.SpiderCrawler.Project
{
    public class CrawlerAllCourse
    {
        public static void GetAllCourse(List<TencentCategoryEntity> categorys)
        {
            List<TencentCategoryEntity> categoryList = categorys;
            var categoryQuery = categoryList.Where(a => !a.Name.Contains("全部"));

            //单线程爬虫
            //foreach (var item in categoryQuery)
            //{
            //    ISearch search = new CourseSearch(item);
            //    search.CrawlerAll();
            //}

            #region 多线程爬虫
            TaskFactory taskFactory = Task.Factory;
            List<Action> actions = new List<Action>();
            foreach (var item in categoryQuery)
            {
                ISearch search = new CourseSearch(item);
                actions.Add(search.CrawlerAll);
            }
            ParallelOptions options = new ParallelOptions();
            options.MaxDegreeOfParallelism = 10;//最多开启10个线程
            Parallel.Invoke(options, actions.ToArray()); 
            #endregion

        }
    }
}
