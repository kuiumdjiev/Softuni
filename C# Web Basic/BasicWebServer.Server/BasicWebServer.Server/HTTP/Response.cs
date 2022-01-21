using System;
using System.Text;

namespace BasicWebServer.Server.HTTP;

public class Response
{
    public StatusCode StatusCode { get; set; }

    public HeaderCollection HeaderCollection { get; } = new HeaderCollection();

    public string Body { get; set; }

    public Response(StatusCode code)
    {
        this.StatusCode = code;
        this.HeaderCollection.Add("Server", "My server");
        this.HeaderCollection.Add("Date", $"{DateTime.UtcNow:r}");
    }

    public override string ToString()
    {
        var result = new StringBuilder();

        result.AppendLine($"HTTP/1.1 {(int)this.StatusCode} {this.StatusCode}");

        foreach (var header in HeaderCollection)
        {
            result.AppendLine(header.ToString());
        }

        result.AppendLine();

        if (!string.IsNullOrEmpty(this.Body))
        { result.Append(this.Body); }

        return result.ToString();
    }
}