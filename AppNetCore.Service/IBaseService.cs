using System;
using System.Collections.Generic;
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

    }
}
