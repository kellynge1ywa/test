using Microsoft.AspNetCore.Mvc;
using Models;

namespace test;
[Route("api/[controller]")]
[ApiController]

public class usersController:ControllerBase
{
    private readonly Iuser userRepository;
    private readonly ResponseDto response;
    public usersController(Iuser iuser)
    {
        userRepository=iuser;
        response=new ResponseDto();


    }
    [HttpGet]
    public async Task<ActionResult<ResponseDto>> GetUsers()
    {
        try
        {
            var users= await userRepository.GetUsers();
            if (users !=null)
            {
                response.Result=users;
                return Ok(response);
            }
             response.Error="Users not found";
             return NotFound(response);

        }
        catch (Exception ex)
        {
            response.Error = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
            return StatusCode(500, response);
        }
    }

    [HttpGet("{Id}")]
    public async Task<ActionResult<ResponseDto>> GetUser(Guid Id)
    {
        try
        {
            var user= await userRepository.GetUser(Id);
            if (user != null)
            {
                response.Result=user;
                return Ok(response);
            }
            response.Error="User not found";
            return NotFound(response);

        }
        catch (Exception ex)
        {
            response.Error = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
            return StatusCode(500, response);
        }
    }

    [HttpPost]
    public async Task<ActionResult<ResponseDto>> AddUser(AddUser user)
    {
        try
        {
            var newUser= new User ()
            {
                Id=new Guid(),
                FirstName=user.FirstName,
                LastName=user.LastName,
                Email=user.Email,
                Age=user.Age

            };
            // var existingUser= await userRepository.GetUser(newUser.Id);
            // if(existingUser !=null)
            // {
            //     response.Error=$"{existingUser.FirstName} {existingUser.LastName} exists";
            //     return BadRequest(response);
            // }
            var added_User= await userRepository.AddUser(newUser);

            response.Result=added_User;
            return Created("", response);

        }

        catch (Exception ex)
        {
            response.Error = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
            return StatusCode(500, response);
        }
    }

    [HttpPut("{Id}")]
    public async Task<ActionResult<ResponseDto>> UpdateUser(Guid Id,AddUser updateUser)
    {
        try
        {
            var toBeUpdatedUser= await userRepository.GetUser(Id);
            if (toBeUpdatedUser == null)
            {
                response.Error="User not found";
                return NotFound(response);
            }
            var update_User=new User()
            {
                Id=toBeUpdatedUser.Id,
                FirstName=updateUser.FirstName,
                LastName=updateUser.LastName,
                Email=updateUser.Email,
                Age=updateUser.Age

            };

            var updatedUser= await userRepository.UpdateUser(toBeUpdatedUser.Id,update_User);
            response.Result=updatedUser;
            return Ok(response);

        }
        catch (Exception ex)
        {
             response.Error = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
            return StatusCode(500, response);
        }
    }

    [HttpDelete("{Id}")]
    public async Task<ActionResult<ResponseDto>> DeleteUser(Guid Id)
    {
        try
        {
            var user= await userRepository.GetUser(Id);
            if(user == null)
            {
                response.Error="User not found";
                return NotFound(response);
            }

            var toBeDeletedUser= await userRepository.DeleteUser(user);
            response.Result=toBeDeletedUser;
            return Ok(response);

        }
        catch (Exception ex)
        {
             response.Error = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
            return StatusCode(500, response);
        }
    }

}
