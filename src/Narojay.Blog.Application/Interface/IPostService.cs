using Narojay.Blog.Domain.Models.Dto;
using Narojay.Tools.Core.Dto;

namespace Narojay.Blog.Application.Interface;

public interface IPostService
{
    Task<bool> AddPostAsync(PostDto postDto);
    Task<PostDto> GetPostByIdAsync(int id);
    Task<PageOutputDto<PostDto>> GetPostListAsync(PageInputBaseDto pageInputBaseDto);
    Dictionary<string, int> GetLabelStatistics();
    Task<PageOutputDto<PostAdminDto>> GetPostAdminAsync(PostAdminDtoRequest request);
    Task<List<StatisticDto>> GetStatisticDtoAsync();
    Task<List<string>> GetLabelsAsync();
    Task<bool> DeleteArticleById(int id);
    Task<string> GetAboutMeContentAsync();
    Task<bool> ModifiyAboutMeContentAsync(AboutMeDto aboutMeDto);
    Task<bool> AddLikeOrUnlikeCountAsync(int id, LikeOrUnlike status);

    Task<bool> AddTagAsync(int id, string name);

    Task<IList<IdAndNameDto>> GetTagsAsync();

    Task<bool> DeleteTagAsync(int id);
    Task<bool> AddTagAsync(TagDto tag);
    Task<bool> AddPostTagAsync(PostTagDto postTagDto);
}