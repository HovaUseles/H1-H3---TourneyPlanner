using TourneyPlanner.API.DTOs;

namespace TourneyPlanner.API.Repositories
{
    public interface ITournamentRepository
    {
        public Task<IEnumerable<TournamentDto>> GetAll();
        public Task<TournamentDto?> GetById(int id);
        public Task<IEnumerable<TournamentDto>> GetMyTournaments(int id);
        public Task<TournamentDto> Create(UserDto userDto, CreateTournamentDto dto);
        public Task Update(int id, CreateTournamentDto dtoChanges);
        public Task Delete(int id);
    }
}
