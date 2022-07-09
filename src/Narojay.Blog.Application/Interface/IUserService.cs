using Narojay.Blog.Domain.Models.Entity;

namespace Narojay.Blog.Application.Interface;

public interface IUserService
{
    Task<List<User>> GetAllUserAsync();
    Task<bool> AddUserAsync(User user);
    Task<bool> ResetPassword(int id, string password);
}