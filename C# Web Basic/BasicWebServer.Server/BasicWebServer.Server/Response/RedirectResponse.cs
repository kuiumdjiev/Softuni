namespace BasicWebServer.Server.HTTP;

public class RedirectResponse:Response
{
    public RedirectResponse(string _location) : base(StatusCode.Found)
    {
        this.HeaderCollection.Add(Header.Location, _location);
    }
}