
using HtmlAgilityPack;
using MJD.SpiderCrawler.Project.Model;
using MJD.SpiderCrawler.Project.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MJD.SpiderCrawler.Project
{

    public class CourseSearch : ISearch
    {
        private TencentCategoryEntity category = null;

        public CourseSearch(TencentCategoryEntity _categroy)
        {
            category = _categroy;
        }
        public void Crawler()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(category.Url))
                {
                    Console.WriteLine($"分类的链接为空{category.Name}{category.CategoryLevel}");
                }
                else
                {
                    string html = HttpHelper.DownLoad(category.Url, Encoding.UTF8);

                    //解析网页
                    HtmlDocument document = new HtmlDocument();
                    document.LoadHtml(html);
                    //通过xpath解析   

                    string pageXPath = "/html/body/section[1]/div/div[7]/a[@class='page-btn']";
                    HtmlNodeCollection pageNodes = document.DocumentNode.SelectNodes(pageXPath);
                    int pageCount = pageNodes.Select(a => Convert.ToInt32(a.InnerText)).OrderByDescending(a => a).FirstOrDefault();

                    List<CourseEntity> courses = new List<CourseEntity>();
                    for (int i = 1; i <= pageCount; i++)
                    {
                        string pageUrl = $"https://ke.qq.com/course/list/%E6%9E%B6%E6%9E%84%E5%B8%88?page={i}";
                        html = HttpHelper.DownLoad(pageUrl, Encoding.UTF8);
                        //解析网页
                        HtmlDocument pageDocument = new HtmlDocument();
                        pageDocument.LoadHtml(html);

                        string liXPath = "/html/body/section[1]/div/div[5]/ul/li";
                        HtmlNodeCollection pageLiNodeList = pageDocument.DocumentNode.SelectNodes(liXPath);
                        foreach (var liNode in pageLiNodeList)
                        {
                            var course = GetOneCourse(liNode);
                            courses.Add(course);
                        }
                       if (i == 3) break;
                    }                    
                    string direct = $"{System.AppDomain.CurrentDomain.BaseDirectory}CrawlerFile\\"; ;
                    string sheetName = "course";
                    string Name = $"{sheetName}{DateTime.Now.ToString("yyyyMMddHHmmss")}.xls";// sheetName  + DateTime.Now.ToString("yyyyMMddHHmmss") + ").xls";

                    if (!Directory.Exists(direct))
                    {
                        Directory.CreateDirectory(direct);
                    }
                    ExcelHelper eh = new ExcelHelper(direct + Name);
                    DataTable dtCourses = ListToDataTable.ToDataTable<CourseEntity>(courses);
                    eh.DataTableToExcel(dtCourses, sheetName, true);

                }
            }
            catch (Exception)
            {

                Console.WriteLine("Crawler抓取异常");
            }
        }
        /// <summary>
        /// 获取一个课程对象
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        private CourseEntity GetOneCourse(HtmlNode liNode )
        {
            CourseEntity course = new CourseEntity();
            ////li标签
            //string liXPath = "/html/body/section[1]/div/div[5]/ul/li[1]";
            //HtmlNode liLoad = document.DocumentNode.SelectSingleNode(liXPath);

            //获取a 标签
            string aXPath = "//*/a";
            HtmlDocument aDocument = new HtmlDocument();
            aDocument.LoadHtml(liNode.OuterHtml);
            HtmlNode aLoad = aDocument.DocumentNode.SelectSingleNode(aXPath);
            if (aLoad.Attributes["href"] != null)
            {
                var aUrl = aLoad.Attributes["href"].Value;
                Console.WriteLine(aUrl);
                course.Url = aUrl;

                var aId = aLoad.Attributes["data-id"].Value;
                Console.WriteLine(aId);
                course.CourseId = Convert.ToInt64(aId);
            }

            //获取a 标签下的img标签
            string imgXPath = "//*/a/img";
            HtmlDocument imgDocument = new HtmlDocument();
            imgDocument.LoadHtml(liNode.OuterHtml);
            HtmlNode imgLoad = imgDocument.DocumentNode.SelectSingleNode(imgXPath);
            if (imgLoad.Attributes["src"] != null)
            {
                var imgUrl = imgLoad.Attributes["src"].Value;
                Console.WriteLine(imgUrl);
                course.ImageUrl = imgUrl;
                var imgTitle = imgLoad.Attributes["title"].Value;
                Console.WriteLine(imgTitle);
                course.Title = imgTitle;

            }

            return course;
        }
    }
}
