using TourneyPlanner.API.DTOs;

namespace TourneyPlanner.API.Repositories
{
    public interface ITournamentRepository
    {
        public Task<IEnumerable<TournamentDto>> GetAll();
        public Task<TournamentDto> GetById(int id);
        public Task Create();
        public Task Update();
        public Task Delete();
    }
}
