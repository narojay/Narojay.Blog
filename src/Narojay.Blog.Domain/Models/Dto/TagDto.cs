using System.ComponentModel.DataAnnotations;

namespace Narojay.Blog.Domain.Models.Dto;

public class TagDto
{
    public int Id { get; set; }

    [Required(ErrorMessage = "标签名必填")] public string Name { get; set; }
}