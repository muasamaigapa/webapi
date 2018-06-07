using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Models
{
    [Table("Classes")]
    public class Classes : Entity<int>
    {
        [Required]
        [MaxLength(100)]
        public string Code { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }

        public virtual ICollection<Students> Students { get; set; }

        public virtual Teachers HomeroomTeacher { get; set; }
        public virtual Teachers Teacher { get; set; }
    }
}