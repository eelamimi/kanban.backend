namespace Backend.Server.Extension;

public static class HttpRequestExtension
{
    public static string GetAccessTokenFromHeader(this HttpRequest request)
    {
        return request.Headers.Authorization.ToString().Replace("Bearer ", "");
    }

    public static Guid GetUserProfileIdFromHeader(this HttpRequest request)
    {
        return new Guid(request.Headers["UserProfileId"].ToString());
    }
}
