using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Narojay.Blog.Models
{
    [Table("Student")]
    public class Student
    {

        [Column("PriKey")]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Timestamp]
        [Column("VerCol")]
        public virtual byte[] RowVersion { get; set; }

        [Column("Name")]
        public string Name { get; set; }
    }
}
