using System.Collections.Generic;

namespace WebApplication4.ViewModel
{
    public class ErrorsModel
    {
        public List<string> Errors { get; set; }

        public ErrorsModel()
        {
            this.Errors = new List<string>();
        }

        public void Add(string error)
        {
            this.Errors.Add(error);
        }
    }
}