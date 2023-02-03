using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Login server started.");
            
            const int port = 8080;

            Console.WriteLine($"Listening on port {port}.");
            HttpListener server = new HttpListener();
            server.Prefixes.Add($"http://localhost:{port}/");
            server.Start();

            string html = "";
            byte[] buffer = Encoding.UTF8.GetBytes("");

            while (true)
            {
                HttpListenerContext context = server.GetContext();
                HttpListenerRequest request = context.Request;
                HttpListenerResponse response = context.Response;

                Console.WriteLine($"{request.HttpMethod} request '{request.RawUrl}'");

                if (request.HttpMethod == "POST")
                {
                    using (StreamReader r = new StreamReader(request.InputStream))
                    {
                        string query = r.ReadToEnd();
                        Match m = Regex.Match(query, "username=(.*)&password=(.*)&password-confirm=(.*)");

                        if (m.Success)
                        {
                            string username = m.Groups[1].Value;
                            string password = m.Groups[2].Value;
                            string passwordConfirm = m.Groups[3].Value;
                            Console.WriteLine($"Logging in with Username: {username}, Password: {password}, PasswordConfirm: {passwordConfirm}.");

                            if (username == "Brumus14" && password == "SuperSecret123")
                            {
                                html = "Login successful";
                            }

                            else
                            {
                                html = "Login failed";
                            }

                            buffer = Encoding.UTF8.GetBytes(html);
                            response.ContentLength64 = buffer.Length;
                            response.OutputStream.Write(buffer, 0, buffer.Length);
                        }
                    }
                }

                else
                {
                    switch (request.RawUrl)
                    {
                        case "/":
                            buffer = Encoding.UTF8.GetBytes(File.ReadAllText("../../static/index.html"));
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
                }

                response.ContentLength64 = buffer.Length;
                response.OutputStream.Write(buffer, 0, buffer.Length);
            }
        }
    }
}
