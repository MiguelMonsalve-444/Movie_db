using System.Collections;
using System.Net;
using System.Text;
using System.Web;

namespace Movie_db;

public class HttpUtilities
{
    public static async Task Respond(HttpListenerRequest req, HttpListenerResponse res, Hashtable options, int StatusCode, string body)
    {
        byte[] content = Encoding.UTF8.GetBytes(body);
        res.StatusCode = StatusCode;
        res.ContentEncoding = Encoding.UTF8;
        res.ContentType = "text/html";
        res.ContentLength64 = content.LongLength;
        await res.OutputStream.WriteAsync(content);
        res.Close();
    }

    public static async Task ReadRequestFromData(HttpListenerRequest req, HttpListenerResponse res, Hashtable options)
    {
        string? type = req.ContentType ?? "";

        if (type.StartsWith("application/x-wwww-url-encoded"))
        {
            using var sr = new StreamReader(req.InputStream, Encoding.UTF8);
            string body = await sr.ReadToEndAsync();
            var fromData = HttpUtility.ParseQueryString(body);

            options["req.form"] = fromData;


        }
    }
}