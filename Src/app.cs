
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_peliculas;

 public class App
    {
        private HttpListener server;

        public App()
        {
            string host = "http://localhost:5000/";
            server = new HttpListener();
            server.Prefixes.Add(host);
            Console.WriteLine($"Server listening... {host}");
        }

        public async Task Start()
        {
            server.Start();
            while (server.IsListening)
            {
                var ctx = await server.GetContextAsync();
                await HandleContextAsync(ctx);
            }
        }

        public void Stop()
        {
            server.Stop();
            server.Close();
        }

        private async Task HandleContextAsync(HttpListenerContext ctx)
        {
            var req = ctx.Request;
            var res = ctx.Response;

            if (req.HttpMethod == "GET" && req.Url != null && req.Url.AbsolutePath == "/")
            {
                string html = "Hello";
                byte[] content = Encoding.UTF8.GetBytes(html);

                res.StatusCode = (int)HttpStatusCode.OK;
                res.ContentEncoding = Encoding.UTF8;
                res.ContentType = "text/html";
                res.ContentLength64 = content.LongLength;
                await res.OutputStream.WriteAsync(content, 0, content.Length);
                res.Close();
            }
        }
    }
