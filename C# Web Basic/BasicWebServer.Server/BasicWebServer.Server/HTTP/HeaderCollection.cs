using System.Collections;
using System.Collections.Generic;

namespace BasicWebServer.Server.HTTP;

public class HeaderCollection:IEnumerable<Header>
{
    private readonly Dictionary<string, Header> headers = new Dictionary<string, Header>();
    public  int Count=> headers.Count;

    public HeaderCollection()
        => this.headers = new Dictionary<string, Header>();

    public void Add(string name, string value)
    {
        var header = new Header(name, value);

        headers.Add(name, header);
    }
    public IEnumerator<Header> GetEnumerator()
        => this.headers.Values.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator()
        => this.GetEnumerator();


}