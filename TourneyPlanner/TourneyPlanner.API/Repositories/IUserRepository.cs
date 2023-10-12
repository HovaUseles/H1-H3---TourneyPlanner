using TourneyPlanner.API.DTOs;

namespace TourneyPlanner.API.Repositories
{
    public interface IUserRepository
    {
        public Task<IEnumerable<UserDto>> GetAll();
        public Task<UserDto?> GetById(int id);
        public Task<UserDto?> GetByEmail(string email);
        public Task<bool> VerifyLogin(AuthHandlerDto loginDto);
        public Task<UserDto> Create(AuthHandlerDto registerDto);
        public Task Delete(int id);
    }
}
