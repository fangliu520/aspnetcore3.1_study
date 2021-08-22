
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
        public void CrawlerAll()
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

                    string pageXPath = "/html/body/section[1]/div/div[6]/a[@class='page-btn']";
                    HtmlNodeCollection pageNodes = document.DocumentNode.SelectNodes(pageXPath);
                    int pageCount = pageNodes.Select(a => Convert.ToInt32(a.InnerText)).OrderByDescending(a => a).FirstOrDefault();

                    List<CourseEntity> courses = new List<CourseEntity>();
                    for (int i = 1; i <= pageCount; i++)
                    {
                        string pageUrl = $"{category.Url}&page={i}";
                        html = HttpHelper.DownLoad(pageUrl, Encoding.UTF8);
                        //解析网页
                        HtmlDocument pageDocument = new HtmlDocument();
                        pageDocument.LoadHtml(html);
                        string liXPath = "/html/body/section[1]/div/div[4]/ul/li";
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
        private CourseEntity GetOneCourse(HtmlNode liNode)
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

        #region 抓取类目



        public List<TencentCategoryEntity> CrawlerCategory()
        {

            try
            {
                if (string.IsNullOrWhiteSpace(category.Url))
                {
                    Console.WriteLine($"分类的链接为空{category.Name}{category.CategoryLevel}");
                }
                else
                {
                    List<TencentCategoryEntity> categorys = new List<TencentCategoryEntity>();
                    string html = HttpHelper.DownLoad(category.Url, Encoding.UTF8);

                    //解析网页
                    HtmlDocument document = new HtmlDocument();
                    document.LoadHtml(html);
                    //通过xpath解析      
                    string firstXPath = "//*[@id=\"auto-test-1\"]/div[1]/dl/dd";
                    HtmlNodeCollection Nodes = document.DocumentNode.SelectNodes(firstXPath);
                    foreach (var item in Nodes)
                    {
                        categorys.AddRange(FirstCategory(item.InnerHtml, null));
                    }

                    string direct = $"{System.AppDomain.CurrentDomain.BaseDirectory}CrawlerFile\\"; ;
                    string sheetName = "category";
                    string Name = $"{sheetName}{DateTime.Now.ToString("yyyyMMddHHmmss")}.xls";// sheetName  + DateTime.Now.ToString("yyyyMMddHHmmss") + ").xls";

                    if (!Directory.Exists(direct))
                    {
                        Directory.CreateDirectory(direct);
                    }
                    ExcelHelper eh = new ExcelHelper(direct + Name);
                    DataTable dtTencentCategory = ListToDataTable.ToDataTable<TencentCategoryEntity>(categorys);
                    eh.DataTableToExcel(dtTencentCategory, sheetName, true);
                    return categorys;
                }
            }
            catch (Exception)
            {

                Console.WriteLine("Crawler抓取异常");
            }
            return null;
        }

        private int _Count = 0;
        private List<TencentCategoryEntity> FirstCategory(string html, string parentCode)
        {
            List<TencentCategoryEntity> categoryList = new List<TencentCategoryEntity>();

            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(html);
            string namePath = "//a/h2";
            HtmlNode name = doc.DocumentNode.SelectSingleNode(namePath);
            string codePath = "//a";
            HtmlNode codeNode = doc.DocumentNode.SelectSingleNode(codePath);
            string href = codeNode.Attributes["href"].Value;

            string code = string.Empty;
            if (href != null && href.IndexOf("mt=") != -1)
            {
                href = href.Replace(";", "&");
                code = href.Substring(href.IndexOf("mt=") + 3, 4);
            }
            TencentCategoryEntity category = new TencentCategoryEntity()
            {
                ID = _Count++,
                State = 1,
                CategoryLevel = 1,
                Code = code,
                ParentCode = parentCode,
                Name = name.InnerText,
                Url = href
            };
            categoryList.Add(category);
            if (!name.InnerText.Equals("全部"))
            {
                categoryList.AddRange(SecondCategory($"{href}", code));
            }

            return categoryList;
        }
        private List<TencentCategoryEntity> SecondCategory(string url, string parentCode)
        {

            List<TencentCategoryEntity> categoryList = new List<TencentCategoryEntity>();
            string html = HttpHelper.DownLoad(url, Encoding.UTF8);
            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(html);
            //通过xpath解析   
            //二级类目
            string secondXPath = "//*[@id=\"auto-test-1\"]/div[1]/dl/dd";
            HtmlNodeCollection Nodes = document.DocumentNode.SelectNodes(secondXPath);
            foreach (var node in Nodes)
            {
                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(node.InnerHtml);
                string codePath = "//a";
                HtmlNode codeNode = doc.DocumentNode.SelectSingleNode(codePath);
                string href = codeNode.Attributes["href"].Value;


                if (href != null)
                {
                    href = href.Replace(";", "&");
                }

                string code = string.Empty;

                if (href != null && href.IndexOf("mt=") != -1)
                {
                    code = href.Substring(href.IndexOf("mt=") + 3, 4);
                }

                TencentCategoryEntity category = new TencentCategoryEntity()
                {
                    ID = _Count++,
                    State = 1,
                    CategoryLevel = 2,
                    Code = code,
                    ParentCode = parentCode,
                    Name = codeNode.InnerText,
                    Url = href
                };
                categoryList.Add(category);
                if (!codeNode.InnerText.Equals("全部"))
                {
                    categoryList.AddRange(ThirdCategory(href, code));
                }
            }
            return categoryList;
        }
        private List<TencentCategoryEntity> ThirdCategory(string url, string parentCode)
        {
            List<TencentCategoryEntity> categoryList = new List<TencentCategoryEntity>();
            string html = HttpHelper.DownLoad(url, Encoding.UTF8);
            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(html);
            //通过xpath解析   
            //三级类目
            string thirdXPath = "//*[@id=\"auto-test-1\"]/div[1]/dl/dd";
            HtmlNodeCollection Nodes = document.DocumentNode.SelectNodes(thirdXPath);
            foreach (var node in Nodes)
            {
                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(node.InnerHtml);
                string codePath = "//a";
                HtmlNode codeNode = doc.DocumentNode.SelectSingleNode(codePath);
                string href = codeNode.Attributes["href"].Value;


                if (href != null)
                {
                    href = href.Replace(";", "&");
                }

                string code = string.Empty;

                if (href != null && href.IndexOf("mt=") != -1)
                {
                    code = href.Substring(href.IndexOf("mt=") + 3, 4);
                }

                TencentCategoryEntity category = new TencentCategoryEntity()
                {
                    ID = _Count++,
                    State = 1,
                    CategoryLevel = 3,
                    Code = code,
                    ParentCode = parentCode,
                    Name = codeNode.InnerText,
                    Url = href
                };
                categoryList.Add(category);
            }
            return categoryList;
        }


        #endregion
     

    }
}
