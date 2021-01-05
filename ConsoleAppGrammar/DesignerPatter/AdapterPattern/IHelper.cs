using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleAppGrammar.DesignerPatter.AdapterPattern
{
    public interface IHelper
    {
        void Add<T>(T t);
        void Delete<T>(T t); 
        void Update<T>(T t);
        void Query<T>(T t);

    }
}
