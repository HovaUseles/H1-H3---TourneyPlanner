using FirebaseAdmin.Messaging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TourneyPlanner.API.DTOs;
using TourneyPlanner.API.Models;
using TourneyPlanner.API.Repositories;
using TourneyPlanner.API.Services;

namespace TourneyPlanner.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatchupController : ControllerBase
    {
        private readonly IMatchupRepository _matchupRepository;
        private readonly IUserRepository _userRepository;
        private readonly INotificationService _notificationService;

        public MatchupController(
            IMatchupRepository matchupRepository,
            IUserRepository userRepository,
            INotificationService notificationService)
        {
            _matchupRepository = matchupRepository;
            _userRepository = userRepository;
            _notificationService = notificationService;
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

        [HttpPut("[action]/{matchupId}")]
        public async Task<ActionResult<MatchupDto>> ChangeScore(int matchupId, IEnumerable<MatchupChangeScoreDto> scoreChanges)
        {
            if(!scoreChanges.Any()) 
            {
                return BadRequest("No scores received ");
            }

            try
            {
                await _matchupRepository.UpdateMatchupScore(matchupId, scoreChanges);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }

            // Build notification message and send it to listeners
            Message message = new Message()
            {
                Notification = new Notification()
                {
                    Title = "New Match Result",
                    Body = $"Result for Matchup ID: {matchupId}."
                },
                Topic = "matchup-update",
            };
            await _notificationService.SendNotificationAsync(message);

            MatchupDto? updatedMatchup = await _matchupRepository.GetById(matchupId);

            return Ok(updatedMatchup);
        }
    }
}
