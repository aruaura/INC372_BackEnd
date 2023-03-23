using Microsoft.AspNetCore.Mvc;
using WebAPI.DBContext;
using WebAPI.Models;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class LogoutController : ControllerBase
{
    private readonly ILogger<LogoutController> _logger;
    private readonly DatabaseContext _databaseContext;

    public LogoutController(ILogger<LogoutController> logger, DatabaseContext databaseContext)
    {
        _logger = logger;
        _databaseContext = databaseContext;
    }


    [HttpPost]
    public IActionResult Logout(Login logout)
    {
        try
        {
            var _logout = _databaseContext.User.SingleOrDefault(o => o.Username == logout.Username && o.Password == logout.Password);
            if(_logout != null)
            {
                return Ok(new {message= "Logout Success"});
            }
            else
            {
                return Ok(new {message= "Retry Again"});
            }

        }
        catch (Exception ex)
        {
            return StatusCode(500,new {result = ex.Message, message= "Error"});
        }
    }

}
