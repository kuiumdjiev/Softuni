using System.Collections.Generic;

namespace BasicWebServer.Server.HTTP;

public class HeaderCollection
{
    private readonly Dictionary<string, Header> headers = new Dictionary<string, Header>();
    public  int Count=> headers.Count;

    public void Add(string name, string value)
    {
        var header = new Header(name, value);

        headers.Add(name, header);
    }
}