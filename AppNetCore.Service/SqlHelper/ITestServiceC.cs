using System;
using System.Collections.Generic;
using System.Text;
using AppNetCore.Utility.CustomAop;
namespace AppNetCore.Service.SqlHelper
{
    public interface ITestServiceC
    {
        [LogBefore]
        [LogAfter]
        void show();
    }
}
