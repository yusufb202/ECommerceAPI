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
            if (registerUserDTO == null || string.IsNullOrEmpty(registerUserDTO.Username) || string.IsNullOrEmpty(registerUserDTO.Password))
            {
                return BadRequest("Invalid user data.");
            }

            var user=new User
            {
                Username = registerUserDTO.Username,
                Password = registerUserDTO.Password,
                Email = registerUserDTO.Email!,
                Role="User"
            };
            var createdUser= await _userService.AddUserAsync(user);
            var userDTO = new UserDTO
            {
                Id = createdUser.Id,
                Username = createdUser.Username,
            };
            return CreatedAtAction(nameof(Register), new { id = createdUser.Id }, userDTO);
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(UserLoginDto userLoginDto)
        {
            var user = await _userService.ValidateUserAsync(userLoginDto.Username, userLoginDto.Password);
            if (user == null)
            {
                return Unauthorized();
            }
            var token = _userService.GenerateJwtToken(user);
            return Ok(token);
        }

    }
}
