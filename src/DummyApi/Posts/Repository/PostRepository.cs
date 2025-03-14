using DummyApi.Posts.Models;
using AutoMapper;

namespace DummyApi.Posts.Repository;

public class PostRepository : IPostRepository
{
    private readonly IMapper _mapper;
    private readonly List<Post> _dummyPosts;

    public PostRepository(IMapper mapper)
    {
        _mapper = mapper;

        _dummyPosts =
        [
            new Post { Id = 1, Title = "Primer Post", Content = "Contenido del primer post", ImagePath = "/images/post1.jpg", Tags = "dummy,ejemplo", CreatedAt = DateTime.Now.AddDays(-5) },
            new Post { Id = 2, Title = "Segundo Post", Content = "Contenido del segundo post", ImagePath = "/images/post2.jpg", Tags = "dummy,test", CreatedAt = DateTime.Now.AddDays(-3) },
            new Post { Id = 3, Title = "Tercer Post", Content = "Contenido del tercer post", ImagePath = "/images/post3.jpg", Tags = "ejemplo,test", CreatedAt = DateTime.Now.AddDays(-1) }
        ];
    }

    public async Task<PostDto> CreatePost(PostDto postDto)
    {
        try
        {
            await Task.Delay(2000);
            var post = _mapper.Map<Post>(postDto);

            post.Id = _dummyPosts.Max(p => p.Id) + 1;
            post.CreatedAt = DateTime.Now;
            _dummyPosts.Add(post);

            return _mapper.Map<PostDto>(post);
        }
        catch
        {
            throw;
        }
    }

    public async Task<bool> DeletePost(int id)
    {
        try
        {
            await Task.Delay(2000);
            var post = _dummyPosts.FirstOrDefault(p => p.Id == id);

            if (post == null)
            {
                return false;
            }

            _dummyPosts.Remove(post);
            return true;
        }
        catch
        {
            throw;
        }

    }

    public async Task<PostDto> GetPostById(int id)
    {
        try
        {
            await Task.Delay(2000);
            var post = _dummyPosts.FirstOrDefault(p => p.Id == id);
            return _mapper.Map<PostDto>(post);
        }
        catch
        {
            throw;
        }

    }

    public async Task<IEnumerable<PostDto>> GetPosts()
    {
        try
        {
            await Task.Delay(2000);
            return _mapper.Map<IEnumerable<PostDto>>(_dummyPosts);
        }
        catch
        {
            throw;
        }
    }

    public async Task<PostDto> UpdatePost(PostDto receviedPost)
    {
        try
        {
            var existingPost = _dummyPosts.FirstOrDefault(p => p.Id == receviedPost.Id);
            if (existingPost == null)
            {
                return null;
            }

            await Task.Delay(2000);
            existingPost.Title = receviedPost.Title;
            existingPost.Content = receviedPost.Content;
            existingPost.ImagePath = receviedPost.ImagePath;
            existingPost.Tags = receviedPost.Tags;
            return _mapper.Map<PostDto>(existingPost);
        }
        catch
        {
            throw;
        }
    }
}