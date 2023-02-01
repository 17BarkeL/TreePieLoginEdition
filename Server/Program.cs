using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Login server started.");

            // Create HTTP server to listen on port 80
            const int port = 8080;

            int pageVisits = 0;

            Console.WriteLine($"Listening on port {port}.");
            HttpListener server = new HttpListener();
            server.Prefixes.Add($"http://localhost:{port}/");
            server.Start();

            while (true)
            {
                HttpListenerContext context = server.GetContext();
                HttpListenerRequest request = context.Request;
                HttpListenerResponse response = context.Response;

                Console.WriteLine($"Request for '{request.RawUrl}'");
                string html = "";
                byte[] buffer = Encoding.UTF8.GetBytes("");

                switch (request.RawUrl)
                {
                    case "/":
                        html = File.ReadAllText("../../static/index.html");
                        break;
                    default:
                        string path = "../../static" + request.RawUrl;

                        if (File.Exists(path))
                        {
                            buffer = File.ReadAllBytes(path);
                        }

                        else
                        {
                            response.StatusCode = 404;
                            html = "404 - File not found.";
                            buffer = Encoding.UTF8.GetBytes(request.RawUrl);
                            Console.WriteLine($"Unknown URL: {request.RawUrl}");
                        }

                        break;
                }


                response.ContentLength64 = buffer.Length;
                response.OutputStream.Write(buffer, 0, buffer.Length);

                pageVisits++;
            }
        }
    }
}
