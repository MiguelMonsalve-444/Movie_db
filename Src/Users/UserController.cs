using System.Collections;
using System.Net;
using System.Security.Cryptography.X509Certificates;

namespace Movie_db;

//using Proyecto_peliculas;

public class UserController
{
    private IUserService userService;



    public UserController(IUserService userService)
    {
        this.userService = userService;
    }

    public async Task ViewAllGet(HttpListenerRequest req, HttpListenerResponse res, Hashtable options)
    {
        int page = int.TryParse(req.QueryString["page"], out int p) ? p : 1;
        int size = int.TryParse(req.QueryString["size"], out int s) ? s : 5;

        Result<PageResult<User>> result = await userService.ReadAll(page, size);

        if (result.IsValid && result.Value != null)
        {
            PageResult<User> pagedResult = result.Value;
            List<User> users = pagedResult.Values;
            int userCount = pagedResult.TotalCount;
            int pageCount = (int)Math.Ceiling((double)userCount / size);

            string rows = "";

            foreach (var user in users)
            {
                rows += @$"
            <tr>

                <td>{user.Id}</td>
                <td>{user.Username}</td>
                <td>{user.Password}</td>
                <td>{user.Salt}</td>
                <td>{user.Role}</td>
            </tr>
            ";
            }

            string html = $@"
            <a href=""/users/add"">Add New User</a>
            <table border=""1"">
            <thead>
               <th>Id</th>
               <th>UserName</th>
               <th>Password</th>
               <th>Salt</th>
               <th>Role</th>
            </thead>
            <tbody>
                {rows}
            </tbody>
        </table>
        <div>
            <a href=""?page=1&size={size}"">First</a>
            <a href=""?page={page - 1}&size={size}"">Prev</a>
            <span>{page} / {pageCount}</span>
            <a href=""?page={page + 1}&size={size}"">Next</a>
            <a href=""?page={pageCount}&size={size}"">Last</a>
        </div>
        ";

            await HttpUtilities.Respond(req, res, options, (int)HttpStatusCode.OK, html);

        }

    }   
    //users/add
    public async Task AddGet(HttpListenerRequest req, HttpListenerResponse res, Hashtable options)
    {
        string roles = "";

        foreach (var role in Roles.ROLES)
        {
            roles += @$"<option valuse=""{role}"">{role}</option>";
        }
        string html = @$"
        
        <form action=""/users/add"" method=""POST"">
            <label for=""username"">Username</label>
            <input id=""username"" name=""username"" type=""text"" placeholder=""Username"">
            <label for=""password"">Password</label>
            <input id=""password"" name=""password"" type=""password"" placeholder=""Password"">
            <label for=""role"">Role</label>
            <select id=""role"" name=""role"">
            {roles}
            </select>
            <input type=""submit"" value=""Add"">
        </form>
        
        ";

        string content = HtmlTemplates.Base("Movie_db", "User Add Page", html);
        await HttpUtilities.Respond(req, res, options, (int)HttpStatusCode.OK, content);
    }
    








    }
