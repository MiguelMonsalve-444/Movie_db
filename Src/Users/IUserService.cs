namespace Movie_db;

public interface IUserService
{
    public Task<Result<PageResult<User>>> ReadAll(int page, int size);
    public Task<Result<User>>Create(User user);
    public Task<Result<User>>Read(int id);
    public Task<Result<User>>Update(int id, User NewUser);
    public Task<Result<User>>Delete(int id);
}