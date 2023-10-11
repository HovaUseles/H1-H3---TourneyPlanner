using Microsoft.EntityFrameworkCore;
using TourneyPlanner.API.DTOs;
using TourneyPlanner.API.Factories.Tournament;
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

        public async Task<TournamentDto> Create(UserDto userDto, CreateTournamentDto dto)
        {
            TournamentType? tournamentType = _context.TournamentTypes.Find(dto.TournamentTypeId);

            if(tournamentType == null)
            {
                throw new ArgumentException($"Invalid tournament type with Id: {dto.TournamentTypeId}.", nameof(dto.TournamentTypeId));
            }

            TournamentFactory factory;

            switch (tournamentType.Name.ToLower())
            {
                case "knockout":
                    factory = new KnockoutTournamentFactory();
                    break;
                default:
                    throw new NotImplementedException($"No implementation for tournament type: {tournamentType.Name}.");
            }

            // Preparing values for tournament construction
            IEnumerable<Matchup> matchups = factory.BuildMatchups(dto);
            GameType? gameType = _context.GameTypes.Find(dto.GameTypeId);
            if(gameType == null)
            {
                throw new ArgumentException($"No GameType exists with Id: {dto.GameTypeId}", nameof(dto.GameTypeId));
            }

            User? user = await _context.Users.FindAsync(userDto.Id);
            if(user == null)
            {
                throw new ArgumentException($"No User exists with Id: {userDto.Id}", nameof(userDto.Id));
            }

            Tournament tournament = new Tournament 
            { 
                Name = dto.Name,
                StartDate = dto.StartDate,
                GameType = gameType,
                TournamentType = tournamentType,
                User = user,
                Matchups = matchups.ToArray()
            };

            await _context.Tournaments.AddAsync(tournament);
            await _context.SaveChangesAsync();

            return ConvertToDto(tournament);
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
            //List<Tournament> tournaments = await _context.Tournaments
            //    .Include(t => t.GameType)
            //    .Include(t => t.TournamentType)
            //    .Include(t => t.User)
            //    .ToListAsync();

            List<Tournament> tournaments = await _context.Tournaments
                .Include(t => t.User)
                .Include(t => t.TournamentType)
                .Include(t => t.GameType)
                .Include(t => t.Matchups)
                    .ThenInclude(m => m.MatchupTeams)
                        .ThenInclude(mt => mt.Team)
                            .ThenInclude(t => t.Players)
                .ToListAsync();

            IEnumerable<TournamentDto> tournamentDtos = tournaments.ConvertAll<TournamentDto>(t =>
            {
                return ConvertToDto(t);
                //return new TournamentDto
                //{
                //    Id = t.Id,
                //    Name = t.Name,
                //    StartDate = t.StartDate,
                //    Type = t.TournamentType.Name,
                //    GameType = t.GameType.Name,
                //    CreatedBy = new UserDto
                //    {
                //        Id = t.User.Id,
                //        Email = t.User.Email
                //    },
                //    Matchups = t.Matchups.ToList().ConvertAll<MatchupDto>(m =>
                //    {
                //        return new MatchupDto
                //        {
                //            Id = m.Id,
                //            Round = m.Rounds,
                //            Teams = m.MatchupTeams.ToList().ConvertAll<TeamDto>(mt =>
                //            {
                //                return new TeamDto
                //                {
                //                    Id = mt.Team.Id,
                //                    TeamName = mt.Team.Name,
                //                    Score = mt.Score ?? 0,
                //                    Players = mt.Team.Players.ToList().ConvertAll<PlayerDto>(p =>
                //                    {
                //                        return new PlayerDto
                //                        {
                //                            Id = p.Id,
                //                            FirstName = p.FirstName,
                //                            LastName = p.LastName
                //                        };
                //                    })
                //                };
                //            })
                //        };
                //    })
                //};
            }); // End Convert

            return tournamentDtos;
        }

        public async Task<TournamentDto?> GetById(int id)
        {
            Tournament? tournament = await _context.Tournaments
                .Include(t => t.User)
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

            return ConvertToDto(tournament);
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

        private TournamentDto ConvertToDto(Tournament tournament)
        {
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
                Matchups = tournament.Matchups.Reverse().ToList().ConvertAll<MatchupDto>(m =>
                {
                    return new MatchupDto
                    {
                        Id = m.Id,
                        Round = m.Rounds,
                        NextMatchupId = m.NextMatchupId,
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
    }
}
