using TourneyPlanner.API.DTOs;

namespace TourneyPlanner.API.Repositories
{
    public interface ITeamRepository
    {
        public Task<TeamDto?> GetById(int id);
    }
}
