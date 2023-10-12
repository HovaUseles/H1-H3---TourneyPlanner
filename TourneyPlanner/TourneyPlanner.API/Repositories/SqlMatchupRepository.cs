using Microsoft.EntityFrameworkCore;
using TourneyPlanner.API.DTOs;
using TourneyPlanner.API.Models;

namespace TourneyPlanner.API.Repositories
{
    public class SqlMatchupRepository : IMatchupRepository
    {
        private readonly TourneyPlannerDevContext _context;

        public SqlMatchupRepository(TourneyPlannerDevContext context)
        {
            _context = context;
        }

        public async Task FollowMatchup(MatchupDto matchup, UserDto userDto)
        {
            Matchup? match = await _context.Matchups.FindAsync(matchup.Id);

            if (match == null)
            {
                throw new ArgumentNullException(nameof(matchup), $"Matchup with Id: {matchup.Id} does not exists.");
            }
            User? user = await _context.Users.FindAsync(userDto.Id);

            if (user == null)
            {
                throw new ArgumentNullException(nameof(userDto), $"User with Id: {userDto.Id} does not exists.");
            }

            FavoritMatchup favMatch = new FavoritMatchup
            {
                Matchup = match,
                User = user
            };

            await _context.FavoritMatchups.AddAsync(favMatch);

            await _context.SaveChangesAsync();

            return;
        }

        public async Task UnfollowMatchup(MatchupDto matchup, UserDto userDto)
        {
            Matchup? match = await _context.Matchups.FindAsync(matchup.Id);

            if (match == null)
            {
                throw new ArgumentNullException(nameof(matchup), $"Matchup with Id: {matchup.Id} does not exists.");
            }
            User? user = await _context.Users.FindAsync(userDto.Id);

            if (user == null)
            {
                throw new ArgumentNullException(nameof(userDto), $"User with Id: {userDto.Id} does not exists.");
            }

            var temp = _context.FavoritMatchups.Where(x => x.MatchupId == matchup.Id && x.UserId == userDto.Id);

            if (temp.Count() > 1)
            {
                _context.RemoveRange(temp);
            }
            else
            {
                _context.Remove(temp);
            }

            await _context.SaveChangesAsync();

            return;
        }

        public async Task<MatchupDto?> GetById(int id)
        {
            Matchup? match = await _context.Matchups
                .Include(m => m.MatchupTeams)
                    .ThenInclude(mt => mt.Team)
                        .ThenInclude(t => t.Players)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (match == null)
            {
                return null;
            }

            return ConvertToDto(match);
        }

        public async Task UpdateMatchupScore(int matchupId, IEnumerable<MatchupChangeScoreDto> scoreChanges)
        {
            Matchup? match = await _context.Matchups
                .Include(m => m.NextMatchup)
                .Include(m => m.MatchupTeams)
                    .ThenInclude(mt => mt.Team)
                        .ThenInclude(t => t.Players)
                .FirstOrDefaultAsync(m => m.Id == matchupId);

            if (match == null)
            {
                throw new KeyNotFoundException($"Matchup with id: {matchupId} not found.");
            }

            foreach (MatchupChangeScoreDto scoreChange in scoreChanges)
            {
                MatchupTeam? matchupTeam = match.MatchupTeams.FirstOrDefault(mt => mt.TeamId == scoreChange.TeamId);

                if (matchupTeam == null)
                {
                    throw new KeyNotFoundException($"Team with id: {scoreChange.TeamId} not found.");
                }

                matchupTeam.Score = scoreChange.Score;
                var entity = _context.MatchupTeams.Attach(matchupTeam);
                entity.State = EntityState.Modified;
            }

            // Find winner and add to the next matchup, if it exists
            if(match.NextMatchupId != null)
            {
                Team winningTeam = match.MatchupTeams
                    .OrderByDescending(mt => mt.Score).First().Team;

                Matchup nextMatchup = await _context.Matchups.FindAsync(match.NextMatchupId);

                MatchupTeam matchupTeam = new MatchupTeam
                { 
                    Matchup = nextMatchup!,
                    Team = winningTeam,
                    Score = 0
                };

                await _context.MatchupTeams.AddAsync(matchupTeam);
            }

            await _context.SaveChangesAsync();
            return;
        }

        private MatchupDto ConvertToDto(Matchup match)
        {
            return new MatchupDto
            {
                Id = match.Id,
                Round = match.Rounds,
                Teams = match.MatchupTeams.ToList().ConvertAll<TeamDto>(mt =>
                {
                    return new TeamDto
                    {
                        Id = mt.Team.Id,
                        TeamName = mt.Team.Name,
                        Score = mt.Score ?? 0,
                        Players = mt.Team.Players.ToList().ConvertAll<PlayerDto>(p =>
                        {
                            return new PlayerDto
                            {
                                Id = p.Id,
                                FirstName = p.FirstName,
                                LastName = p.LastName,
                            };
                        })
                    };
                })
            };
        }
    }
}
