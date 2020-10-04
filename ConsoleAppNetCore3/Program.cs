using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace ConsoleAppNetCore3
{
    class Program
    {
        static void Main(string[] args)
        {
            string phone = "13116227549qq.com.cn";
            //Regex rx = new Regex(@"^(13[0-9]|14[5-9]|15[012356789]|166|17[0-8]|18[0-9]|19[8-9])[0-9]{8}$");
            Regex rx = new Regex(@"^(1[3-9][0-9])[0-9]{8}$");
            Regex yx = new Regex(@"^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$");
            if (!yx.IsMatch(phone)) //不匹配
            {
                Console.WriteLine($"{phone}手机格式不正确");
            }
            else
            {
                Console.WriteLine($"{phone}手机格式正确");

            }

            //string d = "2020-05-20T00:00:00";

            //DateTime dt = Convert.ToDateTime(d);

            //Console.WriteLine(dt.ToString("yyyy-MM-dd 00:00;00"));

            ////int[] str ={ 1, 2, 3, 4, 5, 6 };
            ////var s =[{ 1:"a"},{2:"b" },{3:"c" }];
            //////var s =["a":1];
            ////string s = "Hello World!";
            ////Console.WriteLine($"{str[1]}");
            //List<Info> arr = new List<Info>();
            //arr.Add(new Info() { num = "a",name="1" });
            //arr.Add(new Info() { num = "b", name = "2" });
            //arr.Add(new Info() { num = "c", name = "3" });
            ////string s = string.Join(',', arr);
            //List<string> s = arr.Where(x => x.num == "a").Select(x => x.name).ToList();

            //Console.WriteLine(s[0]);
            Console.ReadLine();
        }

        public class Info
        {
            public string num { get; set; }
            public string name { get; set; }
        }
    }
}
