using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Models
{
    [Table("Teachers")]
    public class Teachers : Entity<int>
    {
        [Required]
        [MaxLength(100)]
        public string Code { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }

        [InverseProperty("HomeroomTeacher")]
        public ICollection<Classes> HomeroomTeacher { get; set; }

        [InverseProperty("Teacher")]
        public ICollection<Classes> Teacher { get; set; }

    }
}