using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace AppNetCore.Interface.Extend
{
    public interface ICustomConnectionFactory
    {
        IDbConnection GetConnection(WriteAndRead writeAndRead);
    }
}
