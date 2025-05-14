namespace Proyecto_peliculas;
using System;
using System.Collections;
using System.Net;
using System.Text;
using Movie_db;

public class AutenticationControl
{
    public AutenticationControl()
    {

    }

    public async Task LandingPageGet(HttpListenerRequest req, HttpListenerResponse res, Hashtable options)
    {
       
            string html = HtmlTemplates.Base("Movie_db","Landing Page", "Hello world");
            byte[] content = Encoding.UTF8.GetBytes(html);

            res.StatusCode = (int)HttpStatusCode.OK;
            res.ContentEncoding = Encoding.UTF8;
            res.ContentType = "text/html";
            res.ContentLength64 = content.LongLength;
            await res.OutputStream.WriteAsync(content, 0, content.Length);
            res.Close();  
            

        

    }
}