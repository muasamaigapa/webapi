using Model.Models;

namespace WebApplication4.ViewModel
{
    public class TeacherModel
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        public TeacherModel()
        {
        }

        public TeacherModel(Teachers t)
        {
            this.Id = t.Id;
            this.Code = t.Code;
            this.Name = t.Name;
        }
    }

    public class CreateTeacher
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }

    public class UpdateTeacher : CreateTeacher
    {
        public int Id { get; set; }
    }
}