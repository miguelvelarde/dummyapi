using DummyApi.Posts.Models;
using DummyApi.Posts.Repository;

namespace DummyApi.Posts.Services;

public class PostService(IPostRepository postRepository, IPostValidator validator) : IPostService
{
    private readonly IPostRepository _postRepository = postRepository;

    private readonly IPostValidator _validator = validator;

    public async Task<IEnumerable<PostDto>> GetPosts()
    {
        return await _postRepository.GetPosts();
    }

    public async Task<PostDto> GetPostById(int id)
    {
        if (id <= 0)
        {
            return null;
        }

        return await _postRepository.GetPostById(id);
    }

    public async Task<PostDto> CreatePost(PostDto postDto)
    {
        if (!_validator.ValidatePost(postDto))
        {
            return null;
        }

        return await _postRepository.CreatePost(postDto);
    }

    public async Task<PostDto> UpdatePost(PostDto postDto)
    {
        if (!_validator.ValidatePost(postDto))
        {
            return null;
        }

        return await _postRepository.UpdatePost(postDto);
    }

    public async Task<bool> DeletePost(int id)
    {
        if (id <= 0)
        {
            return false;
        }

        return await _postRepository.DeletePost(id);
    }       
}