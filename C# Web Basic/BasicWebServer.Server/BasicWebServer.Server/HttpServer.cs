using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace BasicWebServer.Server
{
    public class HttpServer
    {
        private readonly IPAddress adress;

        private readonly int port;

        private  readonly  TcpListener serverListener;

        public HttpServer(string ip , int port)
        {
            this.adress = IPAddress.Parse(ip);
            this.port = port;

            this.serverListener = new TcpListener(this.adress, this.port);
        }

        public void Start()
        {
            this.serverListener.Start();
            Console.WriteLine($"Server start on {this.port}");
            while (true)
            {
                var connection = this.serverListener.AcceptTcpClient();
                var networkStream = connection.GetStream();
                WriteResponse(networkStream, "Hi from server");
                connection.Close();
            }
        }

        private void WriteResponse(NetworkStream networkStream, string message)
        {
            var contentLength = Encoding.UTF8.GetByteCount(message);
            var response = $@"HTTP/1.1 200 OK
Content-Type: text/plain; charset=UTF-8
Content-Length: {contentLength}

{message}";
            var responseBytes = Encoding.UTF8.GetBytes(response);
            networkStream.Write(responseBytes);
        }

        private string ReadRequest(NetworkStream networkStream)
        {
            var bufferLength = 1024;
            var buffer = new byte[bufferLength];
            var totalBytes = 0;
            var requestBuilder = new StringBuilder();
            do
            {
                var read = networkStream.Read(buffer, 0, bufferLength);
                totalBytes += read;
                if (totalBytes>10*1024)
                {
                    throw new InvalidOperationException("Request is too long");
                }

                requestBuilder.Append(Encoding.UTF8.GetString(buffer, 0, bufferLength));
            } while (networkStream.DataAvailable);

            return requestBuilder.ToString();
        }
    }
}
