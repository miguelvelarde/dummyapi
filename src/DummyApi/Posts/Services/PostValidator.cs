using DummyApi.Posts.Models;

namespace DummyApi.Posts.Services
{
    public class PostValidator : IPostValidator
    {
        public bool ValidatePost(PostDto post)
        {
            if (post == null)
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(post.Title))
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(post.Content))
            {
                return false;
            }

            return true;
        }
    }
}