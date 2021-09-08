using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Narojay.Blog.Models.Entity
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
