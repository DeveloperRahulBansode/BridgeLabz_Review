using BusinessLayer.Interface;
using DataAcessLayer.Entity;
using Microsoft.AspNetCore.Mvc;
using ModelLayer;

namespace UserApiReview.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    public readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("AddUser")]
    public async Task<IActionResult> AddUser(User user)
    {
        try
        {
            if (user == null) 
            {
                return BadRequest(new ResponseModel<string> { IsSuccess = false, Messege = "User Addedd Failed...", Data = "User not Addedd.." } );
            
            }

            await _userService.AddUser(user);
            return Ok(new ResponseModel<string> { IsSuccess=true,Messege="User Addedd SucessFully...",Data="User Addedd.."});
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);

        }
    }

    [HttpGet("GetAllUser")]
    public async Task<IActionResult> GetAllUsers()
    {
        try
        {
           var users =await _userService.GetAllUser();

            if (users == null)
            {
                return BadRequest(new ResponseModel<string> { IsSuccess = false, Messege = "No User Found .", Data = "No User Found..." });

            }

            return Ok(new ResponseModel<IEnumerable<User>> { IsSuccess = true, Messege = "Get All SucessFully...", Data = users });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);

        }

    }

    [HttpGet("GetUserById")]
    public async Task<IActionResult> GetUserById(int id)
    {
        try
        {
            var users = await _userService.GetUserById(id);

            if (users == null)
            {
                return BadRequest(new ResponseModel<string> { IsSuccess = false, Messege = "No User Found .", Data = "No User Found..." });

            }

            return Ok(new ResponseModel<User> { IsSuccess = true, Messege = "User GetById SucessFully...", Data = users });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);

        }

    }

    [HttpGet("GetUserByNameStartWith")]
    public async Task<IActionResult> GetUserByName(string nameStartWith)
    {
        try
        {
            var users = await _userService.GetUserByName(nameStartWith);

            if (users == null)
            {
                return BadRequest(new ResponseModel<string> { IsSuccess = false, Messege = "No User Found .", Data = "No User Found..." });

            }

            return Ok(new ResponseModel<IEnumerable<User>> { IsSuccess = true, Messege = "GetUserByName SucessFully...", Data = users });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);

        }

    }

    [HttpGet("AverageAgeOfUser")]
    public async Task<IActionResult> AverageAgeOfUser()
    {
        try
        {
            var data = await _userService.AverageAgeOfUser();

            if (data!=null)
            {
                return Ok(new ResponseModel<double> { IsSuccess = true, Messege = " Get UserAverage Age ", Data = data });

            }

            return BadRequest(new ResponseModel<string> { IsSuccess = false, Messege = "No User Found .", Data = "No User Found..." });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);

        }

    }




}
