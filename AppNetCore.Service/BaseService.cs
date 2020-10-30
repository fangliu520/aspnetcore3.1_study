
using AppNetCore.Interface.Extend;
using Microsoft.Extensions.Options;
using System.Data;


using AppNetCore.Model;
using AppNetCore.Service.Utility;
using System;
using System.Linq.Expressions;

namespace AppNetCore.Service
{
    public class BaseService : IBaseService
    {
       
        private readonly IDapperHelper _dapperHelper;
        public BaseService(IDapperHelper dapperHelper)
        {
            
            _dapperHelper = dapperHelper;
        }

        public string Get()
        {          

            return "ha";
        }
        public bool UpdateStudent(Student student)
        {
            return _dapperHelper.Update<Student>(student);
        }
        public Student GetStudent(int id)
        {
            return _dapperHelper.GetById<Student>(id);
        }

       

        public bool InsertStudent(Student student)
        {
            return _dapperHelper.Insert<Student>(student);
        }

        public PagedResult<Student> GetPagedResult(Student student, int? pageIndex = null, int? pageSize = null)
        {
            
            return _dapperHelper.GetPagedResult<Student>(student);
        }

        public Student Get(Expression<Func<Student, bool>> expression = null)
        {
            Console.WriteLine("成功调用get方法");
            return _dapperHelper.Get<Student>(expression);
        }

        public PagedResult<Student> GetPagedResult(Student student, Expression<Func<Student, bool>> expression , int? pageIndex = null, int? pageSize = null)
        {
            return _dapperHelper.GetPagedResult<Student>(student,expression);
        }
    }
}
