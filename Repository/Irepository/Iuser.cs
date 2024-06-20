using Models;

namespace test;

public interface Iuser
{
    Task<string> AddUser(User user);
    Task<List<User>> GetUsers();
    Task<User> GetUser(Guid Id);
    Task<string> UpdateUser(Guid Id,User user);
    Task<string> DeleteUser(User user);
}
