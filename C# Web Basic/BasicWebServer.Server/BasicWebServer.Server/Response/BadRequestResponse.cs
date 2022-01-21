namespace BasicWebServer.Server.HTTP;

public class BadRequestResponse:Response
{
    public BadRequestResponse() : base(HTTP.StatusCode.BadRequest)
    {
    }
}