
using API.ViewModels;
using Core.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Services.Services.Interfaces;

namespace API.Controllers
{
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("/api/v1/users/create")]
        public async Task<IActionResult> Create([FromBody] CreateUserViewModel user)
        {
            try
            {
                return Ok();
            }
            catch (DomainException ex)
            {
                
                return BadRequest(ex);
            }
            catch (Exception)
            {
                return StatusCode(500, "Erro");
            }      
        }
    }
}