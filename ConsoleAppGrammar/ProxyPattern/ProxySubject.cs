using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleAppGrammar.ProxyPattern
{
    public class ProxySubject : ISubject
    {
        //private static RealSubject _realSubject =new RealSubject();//单例直接构造
        private static RealSubject _realSubject = null;//延迟加载，对象初始化的时候不占用资源
        private void Init()
        {
            _realSubject = new RealSubject();
        }
        public void DoSomethingLong()
        {
            try
            {
                 
                if (_realSubject == null)
                {
                    Init();
                }
                _realSubject.DoSomethingLong();
            }
            catch (Exception ex)
            {

                Console.WriteLine($"{ex.Message}|{ex.StackTrace}");
            }

        }

        public string GetSomethingLong()
        {
            try
            {
                string key = $"{nameof(ProxySubject)}_{nameof(GetSomethingLong)}";
                string r = "";
                if (!CustomCache.Exist(key))
                {
                    if (_realSubject == null)
                    {
                        Init();
                    }
                    r = _realSubject.GetSomethingLong();
                    CustomCache.Add(key, r);
                }
                else
                {
                    r = CustomCache.Get<string>(key);
                }
                Console.WriteLine($"{this.GetType().Name} {r}");
                return r;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}|{ex.StackTrace}");
                throw new Exception(ex.Message);
            }
        }
    }
}
