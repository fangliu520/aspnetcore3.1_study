using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleAppNetCore3
{
    class Program
    {
        static void Main(string[] args)
        {

            string d = "2020-05-20T00:00:00";

            DateTime dt = Convert.ToDateTime(d);

            Console.WriteLine(dt.ToString("yyyy-MM-dd 00:00;00"));

            //int[] str ={ 1, 2, 3, 4, 5, 6 };
            //var s =[{ 1:"a"},{2:"b" },{3:"c" }];
            ////var s =["a":1];
            //string s = "Hello World!";
            //Console.WriteLine($"{str[1]}");
            List<Info> arr = new List<Info>();
            arr.Add(new Info() { num = "a",name="1" });
            arr.Add(new Info() { num = "b", name = "2" });
            arr.Add(new Info() { num = "c", name = "3" });
            //string s = string.Join(',', arr);
            List<string> s = arr.Where(x => x.num == "a").Select(x => x.name).ToList();
        
            Console.WriteLine(s[0]);
            Console.ReadLine();
        }

        public class Info
        {
            public string num { get; set; }
            public string name { get; set; }
        }
    }
}
