using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace AppNetCore.Utility
{
    public interface IExpressionToSqlWhereHelper
    {
        string Condition(Expression node);
    }
}
