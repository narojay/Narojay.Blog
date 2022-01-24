using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Narojay.Blog.Models.Entity.Test
{
    [Table("test_account")]
    public class TestAccount :BaseEntity
    {
        [Column("user_id")]
        public int UserId { get; set; }

        [Column("account_no")]
        public string AccountNo { get; set; }

        [Column("account_name")]
        public string AccountName { get; set; }
    }
}
