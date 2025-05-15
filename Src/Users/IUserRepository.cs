namespace Movie_db;

public interface IUserRepository
{
    public Task<PageResult<User>> ReadAll(int page, int size);
    public Task<User?> Create(User user);
    public Task<User?> Read(int id);
    public Task<User?> Update(int id, User NewUser);
    public Task<User?> Delete(int id);
}