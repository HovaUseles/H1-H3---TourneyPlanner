using Microsoft.EntityFrameworkCore;
using TourneyPlanner.API.DTOs;
using TourneyPlanner.API.Models;

namespace TourneyPlanner.API.Repositories
{
    public class SqlTournamentRepository : ITournamentRepository
    {
        private readonly TourneyPlannerDevContext _context;

        public SqlTournamentRepository(TourneyPlannerDevContext context)
        {
            _context = context;
        }

        public async Task<TournamentDto> Create(CreateTournamentDto dto)
        {
            throw new NotImplementedException();
        }

        public async Task Delete(int id)
        {
            Tournament? tournament = await _context.Tournaments
                .Include(t => t.TournamentType)
                .Include(t => t.GameType)
                .Include(t => t.Matchups)
                    .ThenInclude(m => m.MatchupTeams)
                        .ThenInclude(mt => mt.Team)
                            .ThenInclude(t => t.Players)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (tournament == null)
            {
                throw new KeyNotFoundException($"No tournament with Id: {id}");
            }
            _context.Tournaments.Remove(tournament);
            return;
        }

        public async Task<IEnumerable<TournamentDto>> GetAll()
        {
            List<Tournament> tournaments = await _context.Tournaments
                .Include(t => t.GameType)
                .Include(t => t.TournamentType)
                .Include(t => t.User)
                .ToListAsync();

            IEnumerable<TournamentDto> tournamentDtos = tournaments.ConvertAll<TournamentDto>(t =>
            {
                return new TournamentDto
                {
                    Id = t.Id,
                    Name = t.Name,
                    StartDate = t.StartDate,
                    Type = t.TournamentType.Name,
                    GameType = t.GameType.Name,
                    CreatedBy = new UserDto
                    {
                        Id = t.User.Id,
                        Email = t.User.Email
                    },
                    Matchups = t.Matchups.ToList().ConvertAll<MatchupDto>(m =>
                    {
                        return new MatchupDto
                        {
                            Id = m.Id,
                            Round = m.Rounds,
                            Teams = m.MatchupTeams.ToList().ConvertAll<TeamDto>(mt =>
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
                                            LastName = p.LastName
                                        };
                                    })
                                };
                            })
                        };
                    })
                };
            }); // End Convert

            return tournamentDtos;
        }

        public async Task<TournamentDto?> GetById(int id)
        {
            Tournament? tournament = await _context.Tournaments
                .Include(t => t.TournamentType)
                .Include(t => t.GameType)
                .Include(t => t.Matchups)
                    .ThenInclude(m => m.MatchupTeams)
                        .ThenInclude(mt => mt.Team)
                            .ThenInclude(t => t.Players)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (tournament == null)
            {
                return null;
            }


            return new TournamentDto
            {
                Id = tournament.Id,
                Name = tournament.Name,
                StartDate = tournament.StartDate,
                Type = tournament.TournamentType.Name,
                GameType = tournament.GameType.Name,
                CreatedBy = new UserDto
                {
                    Id = tournament.User.Id,
                    Email = tournament.User.Email
                },
                Matchups = tournament.Matchups.ToList().ConvertAll<MatchupDto>(m =>
                {
                    return new MatchupDto
                    {
                        Id = m.Id,
                        Round = m.Rounds,
                        Teams = m.MatchupTeams.ToList().ConvertAll<TeamDto>(mt =>
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
                                        LastName = p.LastName
                                    };
                                })
                            };
                        })
                    };
                })
            };

        }

        public async Task Update(int id, CreateTournamentDto dtoChanges)
        {
            throw new NotImplementedException();
            //Tournament? tournament = await _context.Tournaments
            //    .Include(t => t.TournamentType)
            //    .Include(t => t.GameType)
            //    .Include(t => t.Matchups)
            //        .ThenInclude(m => m.MatchupTeams)
            //            .ThenInclude(mt => mt.Team)
            //                .ThenInclude(t => t.Players)
            //    .FirstOrDefaultAsync(x => x.Id == id);
            //if (tournament == null)
            //{
            //    throw new KeyNotFoundException($"No tournament with Id: {id}");
            //}

            //Tournament? tournamentChanges = new Tournament
            //{
            //    Id = id,
            //    TournamentType = _context.TournamentTypes.
            //};

            //var entity = _context.Tournaments.Attach();
        }
    }
}
