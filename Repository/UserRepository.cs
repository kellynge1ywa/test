
using Microsoft.EntityFrameworkCore;

namespace test;

public class UserRepository : Iuser
{
    private readonly AppDbContext dbContext;
    public UserRepository(AppDbContext dbContext)
    {
        this.dbContext=dbContext;
    }
    public async Task<string> AddUser(User user)
    {
        try{

            var dbTransaction=  dbContext.Database.BeginTransaction();
            if(string.IsNullOrEmpty(user.FirstName) || string.IsNullOrEmpty(user.LastName))
            {
                return "Please enter firstname and lastname";
            }

            var userNames= dbContext.Users.Where(person=> person.Id != user.Id && person.FirstName.ToLower().Equals(user.FirstName.Trim().ToLower()) && person.LastName.ToLower().Equals(user.LastName)).FirstOrDefault();
            if(userNames != null){
                return  $"{user.FirstName} and {user.LastName} exists";
            }
            // var newUser= new User
            // {
            //     Id=Guid.NewGuid(),
            //     FirstName=user.FirstName,
            //     LastName=user.LastName,
            //     Age=user.Age

            // };

            dbContext.Users.Add(user);
            await dbContext.SaveChangesAsync();
            dbTransaction.Commit();
            return $"{user.FirstName} added successfully";

        
        }
        catch (Exception ex)
        {
            return $"Internal server error {ex}";
        } 
        finally{
            dbContext.Dispose();
        }
    }

    public Task<string> DeleteUser(User user)
    {
        throw new NotImplementedException();
    }

    public Task<User> GetUser(Guid Id)
    {
        throw new NotImplementedException();
    }

    public async Task<List<User>> GetUsers()
    {
        return await dbContext.Users.ToListAsync();
    }

    public Task<string> UpdateUser(User user)
    {
        throw new NotImplementedException();
    }
}
