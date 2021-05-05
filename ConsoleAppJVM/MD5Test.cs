using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenchmarkDotNet;
using BenchmarkDotNet.Attributes;

namespace ConsoleAppJVM
{
   // [MaxColumn, MinColumn, MemoryDiagnoser]
    public class MD5Test
    {

        HashAndMD5 hashAndMD5 = new HashAndMD5();

        string content = "http://www.taobao.com";

        #region 无参数测试
        //[Benchmark]
        //public string GetMD5() => hashAndMD5.GetMD5(content);

        //[Benchmark]
        //public string GetSHA1() => hashAndMD5.GetSHA1(content); 
        #endregion



        #region 带参数测试
        [Params(5, 10)]
        public int Num { get; set; }

        [Benchmark]
        public void GetMD5() => GetMD5(Num);


        [Benchmark]
        public void GetSHA1() => GetSHA1(Num);

        public void GetMD5(int number)
        {
            for (int i = 0; i < number; i++)
            {
                hashAndMD5.GetMD5(content);
            }
        }

        public void GetSHA1(int number)
        {
            for (int i = 0; i < number; i++)
            {
                hashAndMD5.GetSHA1(content);
            }
        } 
        #endregion




    }
}
