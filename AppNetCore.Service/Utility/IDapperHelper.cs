using AppNetCore.Model;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace AppNetCore.Service.Utility
{
    public interface IDapperHelper
    {
        /// <summary>
        /// 根据ID获取单个实例模型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        T GetById<T>(int id) where T : BaseModel;
        /// <summary>
        /// 根据ID更新
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        bool Update<T>(T t) where T : BaseModel, new();
        /// <summary>
        /// 通用插入
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        bool Insert<T>(T t) where T : BaseModel, new();

        /// <summary>
        /// 查询全部
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        IEnumerable<T> GetList<T>(T t) where T : BaseModel, new();

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        PagedResult<T> GetPagedResult<T>(T t, int? pageIndex = null, int? pageSize = null) where T :class ,new();

        #region 根据表达式目录树 通用CRUD操作

        T Get<T>(Expression<Func<T, bool>> expression ) where T: BaseModel, new();
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        PagedResult<T> GetPagedResult<T>(T t,Expression<Func<T,bool>> expression , int? pageIndex = null, int? pageSize = null) where T : class, new();

        #endregion


    }
}
