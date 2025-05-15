using System.Collections;
using System.Net;

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
        int size = int.TryParse(req.QueryString["size"], out int s) ? s : 1;

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
                <td>{user.Role}</td>
                <td>{user.Salt}</td>
            </tr>
            ";
            }

            string html = $@"
        <table>
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
}