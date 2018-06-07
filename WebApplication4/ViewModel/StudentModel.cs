using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication4.ViewModel
{
    public class StudentModel
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public DateTime? BirthDay { get; set; }
        public string Address { get; set; }
        public int Class_Id { get; set; }

        public StudentModel() { }

        public StudentModel(Students st)
        {
            Code = st.Code;
            Name = st.Name;
            BirthDay = st.BirthDay;
            Address = st.Address;
            Class_Id = st.Class.Id;
        }
    }

    public class CreateStudentMdoel
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public DateTime? BirthDay { get; set; }
        public string Address { get; set; }
        public int Class_Id { get; set; }
    }

    public class UpdateStudentModel : CreateStudentMdoel
    {
        public int Id { get; set; }
    }
}