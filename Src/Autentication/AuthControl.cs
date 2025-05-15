namespace Proyecto_peliculas;
using System;
using System.Collections;
using System.Net;
using System.Text;
using System.Web;
using Movie_db;

public class AutenticationControl
{
    private IUserService userService;
    public AutenticationControl(IUserService userService)
    {
        this.userService = userService;
    }

    public async Task LandingPageGet(HttpListenerRequest req, HttpListenerResponse res, Hashtable options)
    {
       
            string html = HtmlTemplates.Base("Movie_db","Landing Page", "Hello world 2");
            
            await HttpUtilities.Respond(req,res,options,(int)HttpStatusCode.OK,html);

    }
}