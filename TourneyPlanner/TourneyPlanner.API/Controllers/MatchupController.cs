using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<ActionResult> FollowMatchup(int matchupId)
        {
            int userId = 1; // TODO: Get From JWT Token
            var user = await _userRepository.GetById(userId);

            MatchupDto matchupDto =  await _matchupRepository.GetById(matchupId);
            await _matchupRepository.FollowMatchup(matchupDto, user);

            return Ok();
        }
    }
}
