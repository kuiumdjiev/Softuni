namespace BasicWebServer.Server.HTTP;

public class UnauthorizedResponse:Response
{
    public UnauthorizedResponse() : base(HTTP.StatusCode.Unauthorized)
    {
    }
}