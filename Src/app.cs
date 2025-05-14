
using System.Collections;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Movie_db;
using Proyecto_peliculas;


 public class App
{
    private HttpListener server;
    private HttpRouter router;
     public App()
    {
        string host = "http://localhost:5000/";
        server = new HttpListener();
        server.Prefixes.Add(host);
        Console.WriteLine($"Server listening... {host}");
        var autenticationController = new AutenticationControl();
        router = new HttpRouter();
        router.AddGet("/", autenticationController.LandingPageGet);        
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
        var options = new Hashtable ();

        await router.Handle(req,res,options);
    }
}