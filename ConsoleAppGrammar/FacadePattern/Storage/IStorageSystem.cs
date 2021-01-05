using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleAppGrammar.FacadePattern.Storage
{
    interface IStorageSystem
    {
        bool CheckStorage( int productId);
        void NewStorage(int productId);
    }
}
