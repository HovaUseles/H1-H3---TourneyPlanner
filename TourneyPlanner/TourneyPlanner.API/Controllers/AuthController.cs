using Microsoft.AspNetCore.Mvc;
using TourneyPlanner.API.DTOs;
using TourneyPlanner.API.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TourneyPlanner.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public AuthController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        [HttpPost("[action]")]
        public async Task<ActionResult> Register(RegisterDto dto)
        {
            UserDto? userExist = await _userRepository.GetByUsername(dto.Username);

            return Ok();
        }
    }
}
