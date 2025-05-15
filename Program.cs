//namespace Proyecto_peliculas;
namespace Movie_db;


public class Program
{
    public static async Task Main()
    {
        App app = new App();
        await app.Start();
    }
}