using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleAppGrammar.FacadePattern.Storage
{
    class StorageSystem : IStorageSystem
    {
        public bool CheckStorage(int productId)
        {
            return productId < 100;
        }

        public void NewStorage( int productId)
        {
            throw new NotImplementedException();
        }
    }
}
