using TourneyPlanner.API.DTOs;

namespace TourneyPlanner.API.Repositories
{
    public interface IUserRepository
    {
        public Task<IEnumerable<UserDto>> GetAll();
        public Task<UserDto> GetById(int id);
        public Task<UserDto?> GetByUsername(string username);
        public Task<bool> VerifyLogin(LoginDto loginDto);
        public Task Create(RegisterDto registerDto);
        public Task Update();
        public Task Delete();
    }
}
