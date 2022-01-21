using BasicWebServer.Server;
using BasicWebServer.Server.HTTP;

await new HttpServer(routes => routes
        .MapGet("/", new TextResponse("Hello from the server!"))
        .MapGet("/HTML", new HtmlResponse("<h1>HTML Response</h1>"))
        .MapGet("/Redirect", new RedirectResponse("https://softuni.org/")))
    .Start();