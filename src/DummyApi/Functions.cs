using Amazon.Lambda.Core;
using Amazon.Lambda.Annotations;
using Amazon.Lambda.Annotations.APIGateway;
using Amazon.Lambda.APIGatewayEvents;
using DummyApi.Posts.Services;
using DummyApi.Posts.Models;
using DummyApi.Common;
using System.Text.Json;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace DummyApi;

public class Functions
{
    private IPostService _postService;

    public Functions(IPostService postService)
    {
        _postService = postService;
    }

    [LambdaFunction()]
    [HttpApi(LambdaHttpMethod.Get, "/posts")]
    public async Task<APIGatewayHttpApiV2ProxyResponse> GetPosts()
    {
        try
        {
            var posts = await _postService.GetPosts();
            return ApiHttpResponse.Ok(posts, "success");
        }
        catch (Exception)
        {
            return ApiHttpResponse.InernalServerError<PostDto>("Internal Server Error");
        }
    }

    [LambdaFunction()]
    [HttpApi(LambdaHttpMethod.Get, "/posts/{id}")]
    public async Task<APIGatewayHttpApiV2ProxyResponse> GetPostById(int id)
    {
        try
        {
            var post = await _postService.GetPostById(id);

            return post != null
                ? ApiHttpResponse.Ok(post, "success")
                : ApiHttpResponse.NotFound<PostDto>("Post not found");
        }
        catch (Exception ex)
        {
            return ApiHttpResponse.InernalServerError<PostDto>(ex.Message);
        }
    }

    [LambdaFunction()]
    [HttpApi(LambdaHttpMethod.Delete, "/posts/{id}")]
    public async Task<APIGatewayHttpApiV2ProxyResponse> DeletePost(int id)
    {
        try
        {
            var result = await _postService.DeletePost(id);
            if (result)
            {
                return ApiHttpResponse.Ok(result, "success");
            }
            else
            {
                return ApiHttpResponse.NotFound<bool>("Post not found");
            }
        }
        catch (Exception ex)
        {
            return ApiHttpResponse.InernalServerError<PostDto>(ex.Message);
        }
    }

    [LambdaFunction()]
    [HttpApi(LambdaHttpMethod.Post, "/posts")]
    public async Task<APIGatewayHttpApiV2ProxyResponse> CreatePost(APIGatewayHttpApiV2ProxyRequest request)
    {
        try
        {
            var postDto = JsonSerializer.Deserialize<PostDto>(request.Body);
            var post = await _postService.CreatePost(postDto);
            return ApiHttpResponse.Ok(post, "success");
        }
        catch (Exception ex)
        {
            return ApiHttpResponse.InernalServerError<PostDto>(ex.Message);
        }
    }

    [LambdaFunction()]
    [HttpApi(LambdaHttpMethod.Put, "/posts/{postId}")]
    public async Task<APIGatewayHttpApiV2ProxyResponse> UpdatePost(APIGatewayHttpApiV2ProxyRequest request, int postId)
    {
        try
        {
            var postDto = JsonSerializer.Deserialize<PostDto>(request.Body);
            return ApiHttpResponse.Ok(await _postService.UpdatePost(postDto), "success");
        }
        catch (Exception ex)
        {
            return ApiHttpResponse.InernalServerError<PostDto>(ex.Message);
        }
    }

}