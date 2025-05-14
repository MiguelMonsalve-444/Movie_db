
using System.Collections;
using System.Net;
namespace Movie_db;


public delegate Task HttpMiddleware(HttpListenerRequest req, HttpListenerResponse res, Hashtable options);
