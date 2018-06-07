using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Models
{
    [Table("Students")]
    public class Students : Entity<int>
    {
        [Required]
        [MaxLength(100)]
        public string Code { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }

        public DateTime? BirthDay { get; set; }

        [MaxLength(255)]
        public string Address { get; set; }

        public virtual Classes Class { get; set; }
    }
}