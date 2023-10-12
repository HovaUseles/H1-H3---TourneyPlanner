using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TourneyPlanner.API.DTOs;
using TourneyPlanner.API.Repositories;

namespace TourneyPlanner.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatchupController : ControllerBase
    {
        private readonly IMatchupRepository _matchupRepository;
        private readonly IUserRepository _userRepository;

        public MatchupController(
            IMatchupRepository matchupRepository,
            IUserRepository userRepository)
        {
            _matchupRepository = matchupRepository;
            _userRepository = userRepository;
        }

        [HttpPost("[action]")]
        [Authorize]
        public async Task<ActionResult> FollowMatchup([FromBody]int matchupId)
        {
            ClaimsIdentity? identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity == null)
            {
                return NotFound("Token not found");
            }

            int userId = Convert.ToInt32(identity.FindFirst("UserId")!.Value);
            UserDto? user = await _userRepository.GetById(userId);

            if (user == null)
            {
                return NotFound("User not found");
            }

            MatchupDto? matchupDto = await _matchupRepository.GetById(matchupId);

            if (matchupDto == null)
            {
                return NotFound("Matchups not found");
            }

            await _matchupRepository.FollowMatchup((MatchupDto)matchupDto, (UserDto)user);

            return Ok();
        }

        [HttpPost("[action]")]
        [Authorize]
        public async Task<ActionResult> UnfollowMatchup([FromBody]int matchupId)
        {
            ClaimsIdentity? identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity == null)
            {
                return NotFound("Token not found");
            }

            int userId = Convert.ToInt32(identity.FindFirst("UserId")!.Value);
            UserDto? user = await _userRepository.GetById(userId);

            if (user == null)
            {
                return NotFound("User not found");
            }

            MatchupDto? matchupDto = await _matchupRepository.GetById(matchupId);

            if (matchupDto == null)
            {
                return NotFound("Matchups not found");
            }

            await _matchupRepository.UnfollowMatchup((MatchupDto)matchupDto, (UserDto)user);

            return Ok();
        }
    }
}
