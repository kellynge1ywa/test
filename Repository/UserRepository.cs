
using Microsoft.EntityFrameworkCore;
using Models;

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

            var existingUser= await dbContext.Users.Where(person=> person.Email == user.Email.Trim().ToLower() ).FirstOrDefaultAsync();
             if(existingUser !=null){
                return  $"{user.Email} exists";
             }

           

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

    public async Task<string> DeleteUser(User user)
    {
        try
        {
            dbContext.Users.Remove(user);
            await dbContext.SaveChangesAsync();
            return $"{user.FirstName} {user.LastName} has been deleted!";

        }
        catch (Exception ex)
        {
            return $"Internal server error {ex}";
        }
    }

    public async Task<User> GetUser(Guid Id)
    {
        return await dbContext.Users.Where(user=>user.Id == Id).FirstOrDefaultAsync();
    }

    public async Task<List<User>> GetUsers()
    {
        return await dbContext.Users.ToListAsync();
    }

    public async Task<string> UpdateUser(Guid Id,User user)
    {
        try
        {
            var toBeUpdated= await dbContext.Users.Where(person=>person.Id == Id).FirstOrDefaultAsync();
            if (toBeUpdated != null)
            {
                toBeUpdated.Id=user.Id;
                toBeUpdated.FirstName=user.FirstName;
                toBeUpdated.LastName=user.LastName;
                toBeUpdated.Email=user.Email;
                toBeUpdated.Age=user.Age;

                await dbContext.SaveChangesAsync();
            return $"{toBeUpdated.FirstName} updated successfully!!";

            }

            return "Product not found";

            // dbContext.Users.Update(toBeUpdated);
            

        }
        catch (Exception ex)
        {
            return $"Internal server error {ex}";
        }
    }
}
