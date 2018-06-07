using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication4.ViewModel
{
    public class ClassModel
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int HomeroomTeacher_Id { get; set; }
        public int Teacher_Id { get; set; }


        public ClassModel() { }
        public ClassModel(Classes c)
        {
            this.Id = c.Id;
            this.Code = c.Code;
            this.Name = c.Name;
            this.HomeroomTeacher_Id = c.HomeroomTeacher.Id;
            this.Teacher_Id = c.Teacher.Id;
        }
    }

    public class CreateClass
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public int HomeroomTeacher_Id { get; set; }
        public int Teacher_Id { get; set; }
    }
    public class UpdateClass : CreateClass
    {
        public int Id { get; set; }
    }
}