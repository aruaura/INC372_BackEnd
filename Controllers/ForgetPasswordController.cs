using Microsoft.AspNetCore.Mvc;
using WebAPI.DBContext;
using WebAPI.Models;

namespace WebAPI.Controllers;

[Route("[controller]")]
[ApiController]
public class ForgetPasswordController : ControllerBase
{
    private readonly ILogger<ForgetPasswordController> _logger;
    private readonly DatabaseContext _databaseContext;

    public ForgetPasswordController(ILogger<ForgetPasswordController> logger, DatabaseContext databaseContext)
    {
        _logger = logger;
        _databaseContext = databaseContext;
    }

    [HttpPut]
    public IActionResult Forget(Forget forget)
    {
        try
            {
                var _forget = _databaseContext.User.SingleOrDefault(o => o.Id == forget.Id && o.Username == forget.Username);
                if(_forget != null)
                {
                    _forget.Password = forget.Password;

                    _databaseContext.User.Update(_forget);
                    _databaseContext.SaveChanges();
                    return Ok(new {message= "Change Password Successful"});
                }
                else
                {
                    return Ok(new {message= "Password Not Change, Retry Again!"});
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new {result = ex.Message, message= "fail"});
            }

    }

}
