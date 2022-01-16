using System;
using System.Collections.Generic;
using System.Linq;

namespace BasicWebServer.Server.HTTP;

public class Request
{
    public Method Method { get; set; }

    public string Url { get; set; }

    public HeaderCollection Headers { get; set; }

    public string Body { get; set; }
    public static Request Parse(string request)
    {
        var lines = request.Split("\r\n");
        var start = lines.First().Split(" ");
        var method = ParseMethod(start[0]);
        var url = start[1];
        var headers = ParseHeader(start.Skip(1));
        var bodyLines = start.Skip(2 + headers.Count).ToArray();
        var body = string.Join("\r\n", bodyLines);
        return new Request()
        {
            Method = method,
            Url = url,
            Headers = headers,
            Body = body
        };
    }

    private static HeaderCollection ParseHeader(IEnumerable<string> headerLines)
    {
        var headersCollection = new HeaderCollection();
        foreach (var headerLine in headerLines)
        {
            if (headerLine == string.Empty)
            {
                break;
            }

            var headerParts = headerLine.Split(":",2);
            if (headerParts.Length != 2)
            {
                throw new InvalidOperationException("Request is not valid");
            }

            var name = headerParts[0];
            var value = headerParts[1];

            headersCollection.Add(name, value);
        }
        return headersCollection;
    }

    private static Method ParseMethod(string method)
    {
        try
        {
            return (HTTP.Method)Enum.Parse(typeof(Method), method, true);
        }
        catch (Exception )
        {
            throw new InvalidOperationException($"Method {method} is not supported");
        }
    }
}