
using AppNetCore.Interface.Extend;
using Microsoft.Extensions.Options;
using System.Data;


using AppNetCore.Model;
using AppNetCore.Service.Utility;

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
    }
}
