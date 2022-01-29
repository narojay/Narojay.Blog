using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Narojay.Blog.Models.Entity.Test
{
    [Table("test_user")]
    public class TestUser :BaseEntity
    {
        [Column("age")]
        public byte Age { get; set; }

        [Column("phone")]
        public string Phone { get; set; }

        [Column("email")]
        public string Email { get; set; }

        [Column("name")]
        public string Name { get; set; }

        public virtual List<TestAccount> TestAccount { get; set; }
    }
}
