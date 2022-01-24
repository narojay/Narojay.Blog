using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Castle.Components.DictionaryAdapter;

namespace Narojay.Blog.Models.Entity
{

    [Table("adminnotice")]
    public class AdminNotice :BaseEntity
    {
        public string Content { get; set; }
    }
}
