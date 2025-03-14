using DummyApi.Posts.Models;

namespace DummyApi.Posts.Services
{
    public interface IPostValidator
    {
        bool ValidatePost(PostDto post);
    }
}