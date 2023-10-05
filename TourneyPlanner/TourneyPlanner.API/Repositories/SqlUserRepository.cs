using TourneyPlanner.API.DTOs;
using TourneyPlanner.API.Models;
using TourneyPlanner.API.Services;

namespace TourneyPlanner.API.Repositories
{
    public class SqlUserRepository : IUserRepository
    {
        private readonly TourneyPlannerDevContext _context;
        private readonly IHashingService _hashingService;
        private readonly ISaltService _saltService;

        public SqlUserRepository(
            TourneyPlannerDevContext context,
            IHashingService hashingService,
            ISaltService saltService
            )
        {
            _context = context;
            _hashingService = hashingService;
            _saltService = saltService;
        }

        public Task Create(RegisterDto registerDto)
        {
            string salt = _saltService.GenerateSalt();
            string passwordHash = _hashingService.HashPassword(registerDto.Password, salt);


            User user = new User()
            {
                Email = registerDto.Username,

            };



            throw new NotImplementedException();
        }

        public Task Delete()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<UserDto>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<UserDto> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<UserDto?> GetByUsername(string username)
        {
            throw new NotImplementedException();
        }

        public Task Update()
        {
            throw new NotImplementedException();
        }

        public Task<bool> VerifyLogin(LoginDto loginDto)
        {
            throw new NotImplementedException();
        }
    }
}
