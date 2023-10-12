using TourneyPlanner.API.DTOs;

namespace TourneyPlanner.API.Repositories
{
    public interface IMatchupRepository
    {

        public Task FollowMatchup(MatchupDto matchup, UserDto user);
        public Task UnfollowMatchup(MatchupDto matchup, UserDto user);
        public Task<MatchupDto?> GetById(int id);
    }
}
