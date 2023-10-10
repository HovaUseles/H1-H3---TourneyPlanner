using TourneyPlanner.API.DTOs;
using TourneyPlanner.API.Models;

namespace TourneyPlanner.API.Factories.Tournament
{
    public abstract class TournamentFactory
    {

        public abstract IEnumerable<Matchup> BuildMatchups(CreateTournamentDto dto);
    }
}
