using System.Collections.Generic;
using BusinessLayer.Interface;
using BusinessLayer.Service;
using DataAcessLayer.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer;

namespace UserApiReview.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserQAController : ControllerBase
    {
        public readonly IUserQAService _userQAService;
        public UserQAController(IUserQAService userQAService)
        {
            _userQAService = userQAService;
        }



        [HttpGet("GetUserByNameStartWith")]
        public async Task<IActionResult> GetUserByNameStartWith(string letter)
        {
            try
            {
                var users = await _userQAService.GetUserByNameStartWith(letter);

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

        [HttpGet("GetUserCount")]
        public async Task<IActionResult> GetUserCount()
        {
            try
            {
                var users = await _userQAService.GetUserCount();

                if (users == 0)
                {
                    return BadRequest(new ResponseModel<string> { IsSuccess = false, Messege = "No User Found .", Data = "No User Found..." });

                }

                return Ok(new ResponseModel<int> { IsSuccess = true, Messege = "The Total User Count Is ...", Data = users });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }

        }
        [HttpGet("GetUserOrderByAssending")]
        public async Task<IActionResult> GetUserOrderByAssending()
        {
            try
            {
                var users = await _userQAService.GetUserOrderByAssending();

                if (users == null)
                {
                    return BadRequest(new ResponseModel<string> { IsSuccess = false, Messege = "No User Found .", Data = "No User Found..." });

                }

                return Ok(new ResponseModel<IEnumerable < User >> { IsSuccess = true, Messege = " Get User Order By Assending  ...", Data = users });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }

        }
        [HttpGet("GetUserOrderByDesending")]
        public async Task<IActionResult> GetUserOrderByDesending()
        {
            try
            {
                var users = await _userQAService.GetUserOrderByDesending();

                if (users == null)
                {
                    return BadRequest(new ResponseModel<string> { IsSuccess = false, Messege = "No User Found .", Data = "No User Found..." });

                }

                return Ok(new ResponseModel<IEnumerable<User>> { IsSuccess = true, Messege = " Ge tUser Order By Desending ...", Data = users });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }

        }

           
        [HttpGet("GetUsersWithBooks")]
        public async Task<IActionResult> GetUsersWithBooks()
        {
            try
            {
                var users = await _userQAService.GetUsersWithBooks();

                if (users == null)
                {
                    return BadRequest(new ResponseModel<string> { IsSuccess = false, Messege = "No User Found .", Data = "No User Found..." });

                }

                return Ok(new ResponseModel<IEnumerable<User>> { IsSuccess = true, Messege = " Get Users Wit hBooks ...", Data = users });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }

        }

        [HttpPost("AddBook")]
        public async Task<IActionResult> AddBook(AddBookModel model)
        {
            try
            {
                if (model == null)
                {
                    return BadRequest(new ResponseModel<string> { IsSuccess = false, Messege = "User Addedd Failed...", Data = "User not Addedd.." });
                }

                await _userQAService.AddBook(model);
                return Ok(new ResponseModel<string> { IsSuccess = true, Messege = "User Addedd SucessFully...", Data = "User Addedd.." });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }

        [HttpGet("GetAllBooks")]
        public async Task<IActionResult> GetAllBooks()
        {
            try
            {
                var users = await _userQAService.GetAllBooks();

                if (users == null)
                {
                    return BadRequest(new ResponseModel<string> { IsSuccess = false, Messege = "No User Found .", Data = "No User Found..." });

                }

                return Ok(new ResponseModel<IEnumerable<Book>> { IsSuccess = true, Messege = " Get  All Books ...", Data = users });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }

        }










    }
}
