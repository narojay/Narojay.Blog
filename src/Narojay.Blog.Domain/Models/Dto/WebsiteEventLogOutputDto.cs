using System;

namespace Narojay.Blog.Domain.Models.Dto;

public class WebsiteEventLogOutputDto
{
    public int Id { get; set; }
    
    public string Name { get; set; }
    
    public DateTime CreationTime { get; set; }
    
    public bool IsDeleted { get; set; }
    
    public string OperatorId { get; set; }
    
    public DateTime? LastModifyTime { get; set; }
}