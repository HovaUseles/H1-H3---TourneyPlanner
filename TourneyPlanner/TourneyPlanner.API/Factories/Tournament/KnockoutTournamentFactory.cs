using System;
using TourneyPlanner.API.DTOs;
using TourneyPlanner.API.Models;

namespace TourneyPlanner.API.Factories.Tournament
{
    public class KnockoutTournamentFactory : TournamentFactory
    {
        public override IEnumerable<Matchup> BuildMatchups(CreateTournamentDto dto)
        {
            int numberOfTeams = dto.Teams.Count();
            if(numberOfTeams < 2) 
            {
                throw new ArgumentException("Not enough teams in tournament Dto. Minimum of 2 teams is required", nameof(dto.Teams));
            }
            // Check if the number of teams is valid for a Knockout tournament
            if (IsPowerOfTwo(numberOfTeams) == false)
            {
                throw new ArgumentException($"The number of teams({numberOfTeams}) is an invalid number and a knockout tournament can not be created", nameof(dto.Teams));
            }

            int numberOfRounds = (int)Math.Ceiling(Math.Log(numberOfTeams, 2));

            // Dummy list for removing team when they are added to matchups
            List<TeamDto> teams = new List<TeamDto>();
            teams.AddRange(dto.Teams);

            List<Matchup> matchups = new List<Matchup>();

            // Building the matchups with a reverse for loop, easier to set next matchup id when building it this way
            for(int round = numberOfRounds; round > 0; round--)
            {
                int numberOfMatchesInRound = numberOfTeams / (int)Math.Pow(2, round);

                for (int matchNumber = 0; matchNumber < numberOfMatchesInRound; matchNumber++)
                {
                    List<MatchupTeam> matchupTeams = new List<MatchupTeam>();

                    // build first round with teams
                    if(round == 1) 
                    {
                        matchupTeams.Add(PickRandomMatchupTeam(teams));
                        matchupTeams.Add(PickRandomMatchupTeam(teams));
                    }

                    Matchup? nextMachup = null;

                    // Set nextmatchup if it is not last round (final matchup)
                    if(round < numberOfRounds)
                    {
                        decimal matchupNumberDivided = matchNumber / 2;
                        int nextMachupIndex = (int)Math.Floor(matchupNumberDivided);

                        // Find the matchups 1 round later in the tournament and look for the correct matchup to link to
                        nextMachup = matchups.Where(m => m.Rounds == round + 1).ElementAt(nextMachupIndex);
                    }

                    Matchup matchup = new Matchup
                    {
                        StartDateTime = DateTime.Now,
                        Rounds = round,
                        MatchupTeams = matchupTeams,
                        NextMatchup = nextMachup
                    };

                    matchups.Add(matchup);
                }
            }

            //matchups.Reverse(); // Reverse the list so the first rounds comes first
            return matchups;
        }

        /// <summary>
        /// Picks a random team from a list of TeamDto, removes it from the source and returns it as a MatchupTeam
        /// </summary>
        /// <param name="teams">The TeamDto source</param>
        /// <returns>The randomly picked teams</returns>
        private MatchupTeam PickRandomMatchupTeam(List<TeamDto> teams) 
        {
            Random random = new Random();
            TeamDto randomTeam = teams[random.Next(0, teams.Count())];
            teams.Remove(randomTeam);

            return new MatchupTeam
            {
                Team = new Team
                {
                    Id = randomTeam.Id,
                    Name = randomTeam.TeamName,
                    Players = randomTeam.Players?.ToList().ConvertAll<Player>(p =>
                    {
                        return new Player
                        {
                            Id = p.Id,
                            FirstName = p.FirstName,
                            LastName = p.LastName
                        };
                    })
                },
                Score = randomTeam.Score,
            };
        }

        /// <summary>
        /// Checks whether or not the number can be achieved with a power of 2
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        private bool IsPowerOfTwo(int number)
        {
            if (number <= 0)
            {
                return false;
            }

            return (number & (number - 1)) == 0;
        }
    }
}
