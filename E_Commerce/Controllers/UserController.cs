using Core.DTOs;
using Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service;

namespace ECommerceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]

        public async Task<ActionResult<UserDTO>> Register (RegisterUserDTO registerUserDTO)
        {
            var user=new User
            {
                Username = registerUserDTO.Username,
                Password = registerUserDTO.Password
            };
            var createdUser= await _userService.AddUserAsync(user);
            var userDTO = new UserDTO
            {
                Id = createdUser.Id,
                Username = createdUser.Username,
            };
            return CreatedAtAction(nameof(Register), new { id = createdUser.Id }, userDTO);
        }

    }
}
