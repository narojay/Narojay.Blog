using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Narojay.Blog.Domain.Models.Entity.Enum;
using Narojay.Blog.Domain.Models.Entity.Test;

namespace Narojay.Blog.Domain.Models.Entity;

[Table("User")]
public class User : BaseEntity
{
    [MaxLength(20)] public string UserName { get; set; }

    public Sex? Sex { get; set; }

    [MaxLength(20)] public string NickName { get; set; }

    public int Age { get; set; }

    [MaxLength(50)] public string Email { get; set; }

    [MaxLength(500)] public string Remarks { get; set; }

    [MaxLength(200)] public string Password { get; set; }

    public SampleRole SampleRole { get; set; }

    public virtual ICollection<Post> Posts { get; set; }

    public virtual TestAccount TestAccount { get; set; }
}

public enum SampleRole
{
    Visitor,
    Master
}