namespace Movie_db;

public class MockUserRepository : IUserRepository
{
    public List<User> users;
    private int idCount = 0;

    public MockUserRepository()
    {
        users = [];

    }

    public async Task<PageResult<User>> ReadAll(int page, int size)
    {
        int totalCount = users.Count;
        int Start = Math.Clamp((page - 1) * size, 0, totalCount);
        int Length = Math.Clamp(size, 0, totalCount - Start);
        List<User> values = users.Slice(Start, Length);
        var pageResult = new PageResult<User>(values, totalCount);

        return await Task.FromResult(pageResult);
    }

    public async Task<User?> Create(User user)
    {
        user.Id = idCount++;
        users.Add(user);
        
       

       return await Task.FromResult(user);
    }
    public async Task<User?> Read(int id)
    {
         User? user = users.FirstOrDefault((u)=> u.Id == id);

         return await Task.FromResult(user);
    }

        
    public async Task<User?> Update(int id, User NewUser)
    {
        User? user = users.FirstOrDefault((u)=> u.Id == id);

        if(user != null)
        {
            user.Username = NewUser.Username;
            user.Password = NewUser.Password;
            user.Salt = NewUser.Salt;
            user.Role = NewUser.Role;
        }

        return await Task.FromResult(user);
    }
    public async Task<User?> Delete(int id)
    {
        User? user = users.FirstOrDefault((u)=> u.Id == id);

        if(user != null)
        {
        users.Remove(user);
        }
        return await Task.FromResult(user);
    }
}