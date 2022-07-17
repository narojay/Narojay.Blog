using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Narojay.Blog.Domain.Models.Entity.Test;

[Table("test_account")]
public class TestAccount : BaseEntity
{
    [Column("user_id")] public int UserId { get; set; }

    [Column("account_no")] public string AccountNo { get; set; }

    [Column("account_name")] public string AccountName { get; set; }

    [Column("price")] public decimal Price { get; set; }

    [Column("test_enum")] public TestEnum? TestEnum { get; set; }
}

[Flags]
public enum TestEnum
{
    A = 1 << 0,
    B = 1 << 1,
    C = 1 << 2,
    D = 1 << 3
}