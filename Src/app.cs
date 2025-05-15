
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
        string host = "http://127.0.0.1:5000/";
        server = new HttpListener();
        server.Prefixes.Add(host);
        Console.WriteLine($"Server listening... {host}");

        var userRepository = new MockUserRepository();
        var userService = new MockUserService(userRepository);
        var userController = new UserController(userService);
        var autenticationController = new AutenticationControl(userService);



        router = new HttpRouter();

        router.AddGet("/", autenticationController.LandingPageGet);
        router.AddGet("/users", userController.ViewAllGet);
        router.AddGet("/users/add", userController.AddGet);     
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