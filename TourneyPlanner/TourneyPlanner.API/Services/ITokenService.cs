using TourneyPlanner.API.DTOs;

namespace TourneyPlanner.API.Services
{
    public interface ITokenService
    {
        public TokenDto BuildNewToken(UserDto user);
    }
}
