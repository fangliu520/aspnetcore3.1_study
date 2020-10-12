using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AppNetCore.Model;
namespace AppNetCore.Service
{
    public interface IBaseService
    {
        string Get();

        Student GetStudent(int id);
        bool UpdateStudent(Student student);

        bool InsertStudent(Student student);

        PagedResult<Student> GetPagedResult(Student student, int? pageIndex = null, int? pageSize = null);

        Student Get(Expression<Func<Student, bool>> expression);

        PagedResult<Student> GetPagedResult(Student student, Expression<Func<Student, bool>> expression, int? pageIndex = null, int? pageSize = null);
    }
}
