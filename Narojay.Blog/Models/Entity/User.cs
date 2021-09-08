using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Narojay.Blog.Models.Entity
{
    [Table("User")]
    public class User : BaseEntity
    {
        public string UserName { get; set; }

        public string NickName { get; set; }
        
        public int Age { get; set; }

        public string Remarks { get; set; }
    }

}
