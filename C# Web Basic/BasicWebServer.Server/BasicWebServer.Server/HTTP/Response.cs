using System;

namespace BasicWebServer.Server.HTTP;

public class Response
{
    public StatusCode StatusCode { get; set; }

    public HeaderCollection HeaderCollection { get; } = new HeaderCollection();

    public string Bodey { get; set; }

    public Response(StatusCode code)
    {
        this.StatusCode = code;
        this.HeaderCollection.Add("Server", "My server");
        this.HeaderCollection.Add("Date", $"{DateTime.UtcNow:r}");
    }
}