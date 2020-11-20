/*************************************************************************************
   * CLR版本：        4.0.30319.42000
   * 类 名 称：       DapperHelper
   * 机器名称：       WIN-AQ2FSGMEQLL
   * 命名空间：       AppNetCore.Service.Utility
   * 文 件 名：       DapperHelper
   * 创建时间：       2020/9/29 14:35:00
   * 作    者：       LIUFANG
   * 说   明：
   * 类型                    命外规则                     说明
   * 命名空间  namespace     Pascal           以.分隔，其中每一个限定词均为Pascal命名方式 如ExcelQuicker.Work
   * 类 class                Pascal           每一个逻辑断点首字母大写 如public class MyHome
   * 接口 interface          IPascal          每一个逻辑断点首字母大写,总是以I前缀开始，后接Pascal命名 如public interface IBankAccount 
   * 方法 method             Pascal           每一个逻辑断点首字母大写如private void SetMember(string)
   * 枚举类型 enum           Pascal           每一个逻辑断点首字母大写
   * 委托 delegate        Pascal           每一个逻辑断点首字母大写局部变量方法的
   * 参数                    Camel            首字母小写，之后Pascal命名如string myName   
   * 修改时间：
   * 修 改 人：
  *************************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;
using AppNetCore.Model;
using Dapper;
using System.Reflection;
using System.Linq;
using AppNetCore.Interface.Extend;
using System.Data;
using System.Data.SqlClient;
using AppNetCore.Service.Extend;
using AppNetCore.Utility.Validate;
using System.Linq.Expressions;
using AppNetCore.Utility;

namespace AppNetCore.Service.Utility
{
    public class DapperHelper : IDapperHelper
    {

        private ICustomConnectionFactory _ConnectionFactory;
        private IExpressionToSqlWhereHelper _expressionToSqlWhereHelper;

        public DapperHelper(ICustomConnectionFactory factory, IExpressionToSqlWhereHelper expressionToSqlWhereHelper)
        {
            _ConnectionFactory = factory;
            _expressionToSqlWhereHelper = expressionToSqlWhereHelper;
        }

        public T Get<T>(Expression<Func<T, bool>> expression) where T : BaseModel, new()
        {
            Type type = typeof(T);           
            string sqlWhere =expression!=null? _expressionToSqlWhereHelper.Condition(expression):"";
            sqlWhere = string.IsNullOrWhiteSpace(sqlWhere) ? "" : $" where {sqlWhere}";
            string columsString = string.Join(",", type.GetProperties().Select(p => $"[{p.Name}]"));
            string sqlStr = $"select {columsString} from [{type.Name}]  {sqlWhere}";
            IDbConnection conStr = _ConnectionFactory.GetConnection(WriteAndRead.Read);
            using (SqlConnection conn = new SqlConnection(conStr.ConnectionString))
            {
                var r = conn.Query<T>(sqlStr).ToList();
                if (r != null && r.Count > 0)
                    return r[0] as T;
            }

            return default(T);
        }

        public T GetById<T>(int id) where T : BaseModel
        {
            Type type = typeof(T);
            string columsString = string.Join(",", type.GetProperties().Select(p => $"[{p.Name}]"));
            string sqlStr = $"select {columsString} from [{type.Name}] where id=@id";
            IDbConnection conStr = _ConnectionFactory.GetConnection(WriteAndRead.Read);
            using (SqlConnection conn = new SqlConnection(conStr.ConnectionString))
            {
                var r = conn.Query<T>(sqlStr, new { id = id }).ToList();
                if (r != null && r.Count > 0)
                    return r[0] as T;
            }

            return default(T);
        }

        public IEnumerable<T> GetList<T>(T t) where T : BaseModel, new()
        {

            throw new NotImplementedException();
        }

        public PagedResult<T> GetPagedResult<T>(T t, int? pageIndex = null, int? pageSize = null) where T : class, new()
        {
            PagedResult<T> pagedResult = new PagedResult<T>();
            pagedResult.PageIndex = pageIndex ?? 1;
            pagedResult.PageSize = pageSize ?? 15;
            Type type = typeof(T);
            string columsString = string.Join(",", type.GetProperties().Select(p => $"[{p.Name}]"));
            string sqlStr = string.Format(@" WITH   t AS ( 
                                                            SELECT   {0} FROM [{1}] WITH ( NOLOCK )
                                                         )
                                                        SELECT  t.* ,p.TotalNum FROM  t ,( SELECT COUNT(0) TotalNum FROM t) p
                                                        ORDER BY t.Id OFFSET ( ( @pageIndex - 1 ) * @pageSize ) ROWS FETCH NEXT @pageSize ROWS ONLY;", columsString, type.Name);
            IDbConnection conStr = _ConnectionFactory.GetConnection(WriteAndRead.Read);
            using (SqlConnection conn = new SqlConnection(conStr.ConnectionString))
            {
                var r = conn.Query(sqlStr, new { pageIndex = pagedResult.PageIndex, pageSize = pagedResult.PageSize });
                if (r != null)
                {
                    List<T> tList = new List<T>();
                    int pageNum = 0;
                    foreach (var v in r)
                    {
                        IDictionary<string, object> s = v as IDictionary<string, object>;
                        pageNum = (int)s["TotalNum"];
                        T newT = new T();
                        foreach (var prop in type.GetProperties())
                        {
                            prop.SetValue(newT, s[prop.Name]);
                        }
                        tList.Add(newT);
                    }
                    pagedResult.Data = tList;
                    pagedResult.TotalNum = pageNum;
                }
                return pagedResult;
            }


        }

        public PagedResult<T> GetPagedResult<T>(T t, Expression<Func<T, bool>> expression , int? pageIndex = null, int? pageSize = null) where T : class, new()
        {
            PagedResult<T> pagedResult = new PagedResult<T>();
            pagedResult.PageIndex = pageIndex ?? 1;
            pagedResult.PageSize = pageSize ?? 15;
            string sqlWhere = expression != null ? _expressionToSqlWhereHelper.Condition(expression) : "";
            sqlWhere = string.IsNullOrWhiteSpace(sqlWhere) ? "" : $" where {sqlWhere}";
            Type type = typeof(T);
            string columsString = string.Join(",", type.GetProperties().Select(p => $"[{p.Name}]"));
            string sqlStr = string.Format(@" WITH   t AS ( 
                                                            SELECT   {0} FROM [{1}] WITH ( NOLOCK ) {2}
                                                         )
                                                        SELECT  t.* ,p.TotalNum FROM  t ,( SELECT COUNT(0) TotalNum FROM t) p
                                                        ORDER BY t.Id OFFSET ( ( @pageIndex - 1 ) * @pageSize ) ROWS FETCH NEXT @pageSize ROWS ONLY;", columsString, type.Name,sqlWhere);
            IDbConnection conStr = _ConnectionFactory.GetConnection(WriteAndRead.Read);
            using (SqlConnection conn = new SqlConnection(conStr.ConnectionString))
            {
                var r = conn.Query(sqlStr, new { pageIndex = pagedResult.PageIndex, pageSize = pagedResult.PageSize });
                if (r != null)
                {
                    List<T> tList = new List<T>();
                    int pageNum = 0;
                    foreach (var v in r)
                    {
                        IDictionary<string, object> s = v as IDictionary<string, object>;
                        pageNum = (int)s["TotalNum"];
                        T newT = new T();
                        foreach (var prop in type.GetProperties())
                        {
                            prop.SetValue(newT, s[prop.Name]);
                        }
                        tList.Add(newT);
                    }
                    pagedResult.Data = tList;
                    pagedResult.TotalNum = pageNum;
                }
                return pagedResult;
            }
        }

        public bool Insert<T>(T t) where T : BaseModel, new()
        {
            Type type = typeof(T);
            Tuple<bool, string> item = DataValidateExtend.ValidateModel(t);
            if (!item.Item1)
            {
                string msg = item.Item2;
                return false;
            }

            string columnStr = string.Join(",", type.GetPropertiesWithNoKey().Select(p => $"{p.Name}"));
            string columnValueStr = string.Join(",", type.GetPropertiesWithNoKey().Select(p => $"@{p.Name}"));
            string sqlStr = $"INSERT INTO [{type.Name}]({columnStr}) values({columnValueStr})";
            IDbConnection conStr = _ConnectionFactory.GetConnection(WriteAndRead.Write);
            using (SqlConnection conn = new SqlConnection(conStr.ConnectionString))
            {
                var r = conn.Execute(sqlStr, t);
                return r > 0;
            }
        }

        public bool Update<T>(T t) where T : BaseModel, new()
        {
            Type type = typeof(T);
            string columnValueString = string.Join(",", type.GetPropertiesWithNoKey().Select(p => $"{p.Name}=@{p.Name}"));
            string sqlStr = $"Update {type.Name} set {columnValueString} where Id=@Id ";
            IDbConnection conStr = _ConnectionFactory.GetConnection(WriteAndRead.Write);
            using (SqlConnection conn = new SqlConnection(conStr.ConnectionString))
            {
                var r = conn.Execute(sqlStr, t);
                return r > 0;
            }
        }
    }
}
