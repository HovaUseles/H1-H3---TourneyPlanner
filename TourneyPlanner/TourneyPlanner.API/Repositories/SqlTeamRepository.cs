using Microsoft.EntityFrameworkCore;
using TourneyPlanner.API.DTOs;
using TourneyPlanner.API.Models;

namespace TourneyPlanner.API.Repositories
{
    public class SqlTeamRepository : ITeamRepository
    {

        private readonly TourneyPlannerDevContext _context;

        public SqlTeamRepository(TourneyPlannerDevContext context)
        {
            _context = context;
        }

        public async Task<TeamDto?> GetById(int id)
        {
            Team? team = await _context.Teams
                .Include(t => t.Players)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (team == null)
            {
                return null;
            }

            return ConvertToDto(team);
        }

        private TeamDto ConvertToDto(Team team)
        {
            return new TeamDto
            {
                Id = team.Id,
                TeamName = team.Name,
                Score = 0,
                Players = team.Players.ToList().ConvertAll<PlayerDto>(p =>
                {
                    return new PlayerDto
                    {
                        Id = p.Id,
                        FirstName = p.FirstName,
                        LastName = p.LastName
                    };
                })
            };
        }
    }
}
