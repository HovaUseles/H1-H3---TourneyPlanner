using Microsoft.EntityFrameworkCore;
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

        public async Task Create(AuthHandlerDto registerDto)
        {
            User? usernameExist = _context.Users.FirstOrDefault(u => u.Email == registerDto.Email);
            if(usernameExist != null)
            {
                throw new ArgumentException("Email already exist", nameof(registerDto.Email));
            }

            string salt = _saltService.GenerateSalt();
            string passwordHash = _hashingService.HashPassword(registerDto.Password, salt);

            User user = new User()
            {
                Email = registerDto.Email,
                PasswordHash = passwordHash,
                Salt = salt
            };

            _context.Add( user );

            return;
        }

        public async Task Delete(int id)
        {
            User? user = _context.Users.FirstOrDefault(u => u.Id == id);

            if(user == null)
            {
                throw new ArgumentException($"No user exist with id: {id}", nameof(id));
            }

            _context.Users.Remove(user);

            return;
        }

        public async Task<IEnumerable<UserDto>> GetAll()
        {
            List<User> users = await _context.Users.ToListAsync();
            return users.ConvertAll<UserDto>(u =>
            {
                return new UserDto {
                    Id = u.Id,
                    Email = u.Email
                };
            });
        }

        public async Task<UserDto?> GetById(int id)
        {
            User? user = await _context.Users.FindAsync(id);

            if(user == null)
            {
                return null;
            }

            return new UserDto
            {
                Id = user.Id,
                Email = user.Email
            };
        }

        public async Task<UserDto?> GetByEmail(string email)
        {
            User? user = await _context.Users.Where(u => u.Email == email).FirstOrDefaultAsync();

            if (user == null)
            {
                return null;
            }

            return new UserDto
            {
                Id = user.Id,
                Email = user.Email
            };
        }
        
        public async Task<bool> VerifyLogin(AuthHandlerDto loginDto)
        {
            User? user = await _context.Users.Where(u => u.Email == loginDto.Email).FirstOrDefaultAsync();

            if (user == null)
            {
                return false;
            }

            string passwordHash = _hashingService.HashPassword(loginDto.Password, user.Salt);

            if(user.PasswordHash == passwordHash) 
            {
                return true;
            }

            return false;
        }
    }
}
