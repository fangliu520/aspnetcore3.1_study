using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleAppGrammar.DesignerPatter.AdapterPattern
{
    class SqlServerHelper : IHelper
    {
        public void Add<T>(T t)
        {
            throw new NotImplementedException();
        }

        public void Delete<T>(T t)
        {
            throw new NotImplementedException();
        }

        public void Query<T>(T t)
        {
            throw new NotImplementedException();
        }

        public void Update<T>(T t)
        {
            throw new NotImplementedException();
        }
    }
}
