using System.Text.Json;
using Amazon.Lambda.APIGatewayEvents;

namespace DummyApi.Common;

public static class ApiHttpResponse
{
    public static APIGatewayHttpApiV2ProxyResponse Ok<T>(T data, string message = "Success")
    {
        return CreateResponse(ApiResponse<T>.Result(data, message), 200);
    }
    
    public static APIGatewayHttpApiV2ProxyResponse NotFound<T>(string message = "Not Found")
    {
        return CreateResponse(ApiResponse<T>.Error(message), 404);
    }
    
    public static APIGatewayHttpApiV2ProxyResponse BadRequest<T>(string message)
    {
        return CreateResponse(ApiResponse<T>.Error(message), 400);
    }

    public static APIGatewayHttpApiV2ProxyResponse InernalServerError<T>(string message)
    {
        return CreateResponse(ApiResponse<T>.Error(message), 500);
    }
    
    private static APIGatewayHttpApiV2ProxyResponse CreateResponse<T>(ApiResponse<T> response, int statusCode)
    {
        return new APIGatewayHttpApiV2ProxyResponse
        {
            StatusCode = statusCode,
            Body = JsonSerializer.Serialize(response),
            Headers = new Dictionary<string, string> { { "Content-Type", "application/json" } }
        };
    }
}