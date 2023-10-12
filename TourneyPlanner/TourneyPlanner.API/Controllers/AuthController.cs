using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using TourneyPlanner.API.DTOs;
using TourneyPlanner.API.Models;
using TourneyPlanner.API.Repositories;
using TourneyPlanner.API.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TourneyPlanner.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;

        public AuthController(
            IUserRepository userRepository,
            ITokenService tokenService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
        }

        [HttpPost("[action]")]
        [AllowAnonymous]
        public async Task<ActionResult<TokenDto>> Register(AuthHandlerDto dto)
        {
            if(string.IsNullOrWhiteSpace(dto.Email)) 
            {
                return BadRequest("Username cannot be null");
            }
            if(string.IsNullOrWhiteSpace(dto.Password))
            {
                return BadRequest("Password cannot be null");
            } 

            UserDto? userExist = await _userRepository.GetByEmail(dto.Email);

            if(userExist != null)
            {
                return BadRequest("Username already exist");
            }

            // Validate password
            if(!ValidatePassword(dto.Password))
            {
                return BadRequest("Password not strong enough. Must be atleast 8 characters long and contain atleast 1 lowercase, 1 uppercase and either 1 number or 1 special character.");
            }

            UserDto? user = await _userRepository.Create(dto);
            TokenDto token = _tokenService.BuildNewToken((UserDto)user);

            return Ok(token);
        }

        private bool ValidatePassword(string password)
        {
            return Regex.Match(password, "^((?=.*\\d)|(?=.*[^a-zA-Z0-9]))+(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,}$").Success;
        }

        [HttpPost("[action]")]
        [AllowAnonymous]
        public async Task<ActionResult<TokenDto>> Login(AuthHandlerDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Email))
            {
                return BadRequest("Email cannot be null");
            }
            if (string.IsNullOrWhiteSpace(dto.Password))
            {
                return BadRequest("Password cannot be null");
            }

            UserDto? user = await _userRepository.GetByEmail(dto.Email);

            if(user == null)
            {
                return BadRequest("Email or password was invalid");
            }

            if(await _userRepository.VerifyLogin(dto) == false)
            {
                return BadRequest("Email or password was invalid");
            }

            TokenDto token = _tokenService.BuildNewToken((UserDto)user);

            return Ok(token);
        }

        [HttpPost("[action]")]
        [Authorize]
        public async Task<ActionResult> ValidateToken()
        {
            return Ok();
        }
    }
}
