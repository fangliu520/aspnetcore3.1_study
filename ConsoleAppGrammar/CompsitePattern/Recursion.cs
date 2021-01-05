using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ConsoleAppGrammar.CompsitePattern
{
    public class Recursion
    {
        public static List<DirectoryInfo> GetDirectoryList(string pathRoot)
        {
            List<DirectoryInfo> dirList = new List<DirectoryInfo>();

            DirectoryInfo dirRoot = new DirectoryInfo(pathRoot);//根目录
            dirList.Add(dirRoot);

            GetDirectoryChild(dirList, dirRoot);

            #region 抽离出GetDirectoryChild 方法
            //DirectoryInfo[] dirList1 = dirRoot.GetDirectories();//一级子目录
            //dirList.AddRange(dirList1);
            //foreach (var dir1 in dirList1)
            //{
            //    DirectoryInfo[] dirList2 = dir1.GetDirectories();//二级子目录
            //    dirList.AddRange(dirList2);
            //    foreach (var dir2 in dirList2)
            //    {
            //        DirectoryInfo[] dirList3 = dir1.GetDirectories();//三级子目录
            //        dirList.AddRange(dirList3);

            //    }
            //} 
            #endregion

            return dirList;

        }
        /// <summary>
        /// 递归获取子文件夹，放入容器
        /// </summary>
        /// <param name="dirList"></param>
        /// <param name="dirParent"></param>
        /// <returns></returns>
        public static List<DirectoryInfo> GetDirectoryChild(List<DirectoryInfo> dirList, DirectoryInfo dirParent)
        {
            DirectoryInfo[] dirListChild= dirParent.GetDirectories();
            dirList.AddRange(dirListChild);
            foreach (var dir in dirListChild)
            {
                GetDirectoryChild(dirList, dir);
            }

            return dirList;
        }
    }
}
