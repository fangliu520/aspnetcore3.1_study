using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;

namespace MJD.SpiderCrawler.Project.Utility
{
    public class DateTableToList
    {
        /// <summary>
        /// DataTableToList<Dictionary<string, object>> 转换为list<字典>
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static List<Dictionary<string, string>> DataTableToListString(DataTable dt)
        {
            var list = new List<Dictionary<string, string>>();
            var dic = new Dictionary<string, string>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                foreach (DataColumn dc in dt.Columns)
                {
                    DataRow row = dt.Rows[i];
                    dic.Add(dc.ColumnName, row[dc.ColumnName] is DBNull ? "" : Convert.ToString( row[dc.ColumnName]));
                }
                list.Add(dic);
                dic = new Dictionary<string, string>();
            }
            return list;
        }
        /// <summary>
        /// DataTableToList<Dictionary<string, object>> 转换为list<字典>
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static List<Dictionary<string, object>> DataTableToList(DataTable dt)
        {
            List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
            Dictionary<string, object> dic = new Dictionary<string, object>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                foreach (DataColumn dc in dt.Columns)
                {
                    DataRow row = dt.Rows[i];
                    dic.Add(dc.ColumnName, row[dc.ColumnName]);
                }
                list.Add(dic);
                dic = new Dictionary<string, object>();
            }
            return list;
        }

        /// <summary>
        /// 获得集合实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static List<T> EntityList<T>(DataTable dt)
        {
            if (dt == null || dt.Rows.Count == 0)
            {
                return null;
            }
            List<T> list = new List<T>();
            T entity = default(T);
            foreach (DataRow dr in dt.Rows)
            {
                entity = Activator.CreateInstance<T>();
                PropertyInfo[] pis = entity.GetType().GetProperties();
                foreach (PropertyInfo pi in pis)
                {
                    if (dt.Columns.Contains(pi.Name))
                    {
                        if (!pi.CanWrite)
                        {
                            continue;
                        }
                        if (dr[pi.Name] != DBNull.Value)
                        {
                            Type t = pi.PropertyType;
                            if (t.FullName == "System.Guid")
                            {
                                pi.SetValue(entity, Guid.Parse(dr[pi.Name].ToString()), null);
                            }
                            else
                            {
                                pi.SetValue(entity, dr[pi.Name], null);
                            }

                        }
                    }
                }
                list.Add(entity);
            }
            return list;
        }

        public static List<T> ConvertTableToObject<T>(DataTable t) where T : new()
        {
            List<T> list = new List<T>();
            foreach (DataRow row in t.Rows)
            {
                T obj = ConvertToObject<T>(row);
                list.Add(obj);
            }

            return list;
        }

        public static T ConvertToObject<T>(DataRow row) where T : new()
        {
            object obj = new T();
            if (row != null)
            {
                DataTable t = row.Table;
                GetObject(t.Columns, row, obj);
            }
            if (obj != null && obj is T)
                return (T)obj;
            else
                return default(T);

        }

        private static void GetObject(DataColumnCollection cols, DataRow dr, Object obj)
        {
            Type t = obj.GetType(); //This is used to do the reflection  

            PropertyInfo[] props = t.GetProperties();
            foreach (PropertyInfo pro in props)
            {
                if (cols.Contains(pro.Name))
                {
                    pro.SetValue(obj,
                        dr[pro.Name] == DBNull.Value ? null : dr[pro.Name],
                        null);
                }
            }
        }/// <summary>   
        /// Datatable转换为Json 
        /// </summary>      
        /// <param name="table">Datatable对象</param>
        /// <returns>Json字符串</returns>    
        public static string ConvertToJson(DataTable dt)
        {
            StringBuilder jsonString = new StringBuilder();
            jsonString.Append("[");
            DataRowCollection drc = dt.Rows;
            for (int i = 0; i < drc.Count; i++)
            {
                jsonString.Append("{");
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    string strKey = dt.Columns[j].ColumnName;
                    string strValue = drc[i][j].ToString();
                    Type type = dt.Columns[j].DataType;
                    jsonString.Append("\"" + strKey + "\":");
                    strValue = StringFormat(strValue, type);
                    if (j < dt.Columns.Count - 1)
                    {
                        jsonString.Append(strValue + ",");
                    }
                    else
                    {
                        jsonString.Append(strValue);
                    }
                }
                jsonString.Append("},");
            }
            jsonString.Remove(jsonString.Length - 1, 1);
            jsonString.Append("]");
            return jsonString.ToString();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="jsonName"></param>
        /// <returns></returns>
        public static string ConvertToJson(DataTable dt, string jsonName)
        {
            StringBuilder Json = new StringBuilder();
            if (string.IsNullOrEmpty(jsonName))
                jsonName = dt.TableName;
            Json.Append("{\"" + jsonName + "\":[");
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Json.Append("{");
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        Type type = dt.Rows[i][j].GetType();
                        Json.Append("\"" + dt.Columns[j].ColumnName.ToString() + "\":" + StringFormat(dt.Rows[i][j].ToString(), type));
                        if (j < dt.Columns.Count - 1)
                        {
                            Json.Append(",");
                        }
                    }
                    Json.Append("}");
                    if (i < dt.Rows.Count - 1)
                    {
                        Json.Append(",");
                    }
                }
            }
            Json.Append("]}");
            return Json.ToString();
        }
        /// <summary>     
        /// 格式化字符型、日期型、布尔型 
        /// </summary>     
        /// <param name="str"></param>   
        /// <param name="type"></param> 
        /// <returns></returns>     
        private static string StringFormat(string str, Type type)
        {
            if (type == typeof(string))
            {
                str = String2Json(str);
                str = "\"" + str + "\"";
            }
            else if (type == typeof(DateTime))
            {
                str = "\"" + str + "\"";
            }
            else if (type == typeof(bool))
            {
                str = str.ToLower();
            }
            else if (type != typeof(string) && string.IsNullOrEmpty(str))
            {
                str = "\"" + str + "\"";
            }
            return str;
        }
        /// <summary>     
        /// 过滤特殊字符    
        /// </summary>    
        /// <param name="s">字符串</param> 
        /// <returns>json字符串</returns> 
        private static string String2Json(String s)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < s.Length; i++)
            {
                char c = s.ToCharArray()[i];
                switch (c)
                {
                    case '\"':
                        sb.Append("\\\""); break;
                    case '\\':
                        sb.Append("\\\\"); break;
                    case '/':
                        sb.Append("\\/"); break;
                    case '\b':
                        sb.Append("\\b"); break;
                    case '\f':
                        sb.Append("\\f"); break;
                    case '\n':
                        sb.Append("\\n"); break;
                    case '\r':
                        sb.Append("\\r"); break;
                    case '\t':
                        sb.Append("\\t"); break;
                    default:
                        sb.Append(c); break;
                }
            }
            return sb.ToString();
        }
    }

    public class ListToDataTable {

        public static DataTable ToDataTable<T>(List<T> items)
        {
            var tb = new DataTable(typeof(T).Name);

            PropertyInfo[] props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (PropertyInfo prop in props)
            {
                Type t = GetCoreType(prop.PropertyType);
                tb.Columns.Add(prop.Name, t);
            }

            foreach (T item in items)
            {
                var values = new object[props.Length];

                for (int i = 0; i < props.Length; i++)
                {
                    values[i] = props[i].GetValue(item, null);
                }

                tb.Rows.Add(values);
            }

            return tb;
        }

        public static Type GetCoreType(Type t)
        {
            if (t != null && IsNullable(t))
            {
                if (!t.IsValueType)
                {
                    return t;
                }
                else
                {
                    return Nullable.GetUnderlyingType(t);
                }
            }
            else
            {
                return t;
            }
        }

        public static bool IsNullable(Type t)
        {
            return !t.IsValueType || (t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>));
        }
    }
}
