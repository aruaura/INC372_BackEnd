using Microsoft.AspNetCore.Mvc;
using WebAPI.DBContext;
using WebAPI.Models;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class LoginController : ControllerBase
{
    private readonly ILogger<LoginController> _logger;
    private readonly DatabaseContext _databaseContext;

    public LoginController(ILogger<LoginController> logger, DatabaseContext databaseContext)
    {
        _logger = logger;
        _databaseContext = databaseContext;
    }


    [HttpPost]
    public IActionResult Login(Login login)
    {
        try
        {
            var _login = _databaseContext.User.SingleOrDefault(o => o.Username == login.Username && o.Password == login.Password);
            if(_login != null)
            {
                return Ok(new {message= "Login Success"});
            }
            else
            {
                return Ok(new {message= "Not Found"});
            }

        }
        catch (Exception ex)
        {
            return StatusCode(500,new {result = ex.Message, message= "Login Fail"});
        }
    }

}
