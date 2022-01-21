namespace BasicWebServer.Server.HTTP;

public class NotFoundResponse:Response
{
    public NotFoundResponse() : base(HTTP.StatusCode.NotFound)
    {
    }
}