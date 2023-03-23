using Microsoft.AspNetCore.Mvc;
using WebAPI.DBContext;
using WebAPI.Models;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly ILogger<UserController> _logger;
    private readonly DatabaseContext _databaseContext;

    public UserController(ILogger<UserController> logger, DatabaseContext databaseContext)
    {
        _logger = logger;
        _databaseContext = databaseContext;
    }

    [HttpGet]   // get data from the server -> http://localhost:5133/User
    public IActionResult GetUser()
    {
        try
        {
            var result = _databaseContext.User.ToList();
            return Ok(new {result = result, message="success"});
        }
        catch (Exception ex)
        {
            return StatusCode(500, new {result = ex.Message, message = "fail"});
        }
    }


    [HttpGet("{id}")]
    public IActionResult GetUserById(int id)
    {
        try
        {
            var results = _databaseContext.User.SingleOrDefault(o => o.Id == id);
            return Ok(new {result = results, message="success"});
        }
        catch (Exception ex)
        {
            return StatusCode(500, new {result = ex.Message, message="fail"});
        }
    }


    [HttpPost]   // get data from the server -> http://localhost:5133/User
    public IActionResult CreateUser(User user)
    {
        try
        {
            _databaseContext.User.Add(user);
            _databaseContext.SaveChanges();
            return Ok(new {message= "success"});
        }
        catch (Exception ex)
        {
            return StatusCode(500, new {result = ex.Message, message = "fail"});
        }
    }

    [HttpPut] // -> https://localhost:5001/user
        public IActionResult UpdateUser(User user)
        {
            try
            {
                var _user = _databaseContext.User.SingleOrDefault(o => o.Id == user.Id);
                if(_user != null)
                {
                    _user.Username = user.Username;
                    _user.Password = user.Password;

                    _databaseContext.User.Update(_user);
                    _databaseContext.SaveChanges();
                    return Ok(new {message= "success"});
                }
                else
                {
                    return Ok(new {message= "fail"});
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new {result = ex.Message, message= "fail"});
            }
        }

    [HttpDelete("{id}")] // -> https://localhost:5001/user/1
    public IActionResult DeleteUser(int id)
    {
        try
        {
            var _user = _databaseContext.User.SingleOrDefault(o => o.Id == id);
            if(_user != null)
            {
                _databaseContext.User.Remove(_user);
                _databaseContext.SaveChanges();
                return Ok(new {message= "success"});
            }
            else
            {
                return Ok(new {message= "fail"});
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, new {result = ex.Message, message= "fail"});
        }
    }


}
