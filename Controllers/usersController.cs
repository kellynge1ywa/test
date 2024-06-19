using Microsoft.AspNetCore.Mvc;

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
                Age=user.Age

            };
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

}
