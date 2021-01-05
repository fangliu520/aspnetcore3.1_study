using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleAppGrammar.ProxyPattern
{
   public class CustomCache
    {
        private static Dictionary<string, object> CustomCacheDictionary = new Dictionary<string, object>();

        public static void Add(string key, object value)
        {
            CustomCacheDictionary.Add(key,value);
        }

        public static T Get<T>(string key)
        {
            return (T)CustomCacheDictionary[key];
        }

        public static bool Exist(string key)
        {
            return CustomCacheDictionary.ContainsKey(key);
        }
    }
}
