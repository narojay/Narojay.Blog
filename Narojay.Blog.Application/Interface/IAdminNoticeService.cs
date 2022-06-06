namespace Narojay.Blog.Application.Interface;

public interface IAdminNoticeService
{
    Task<string> GetAdminNoticeAsync();
    Task<bool> EditAdminNoticeAsync(string content);
    Task TestTranscation();
}