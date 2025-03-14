using DummyApi.Posts.Models;

namespace DummyApi.Posts.Repository;

public interface IPostRepository
{
    Task<IEnumerable<PostDto>> GetPosts();
    Task<PostDto> GetPostById(int id);
    Task<PostDto> CreatePost(PostDto post);
    Task<PostDto> UpdatePost(PostDto post);
    Task<bool> DeletePost(int id);
}