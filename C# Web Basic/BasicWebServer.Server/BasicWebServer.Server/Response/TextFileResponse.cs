using System.Net.Mime;

namespace BasicWebServer.Server.HTTP;

public class TextFileResponse:Response
{
    public string FullName { get; set; }

    public TextFileResponse(string fileName) : base(StatusCode.OK)
    {
        this.FullName = fileName;
        this.HeaderCollection.Add(Header.ContentType, ContentType.PlainText);
    }

    public override string ToString()
    {
        return base.ToString();
    }
}