using TourneyPlanner.API.Models;

namespace TourneyPlanner.API.DTOs
{
    public record struct CreateTournamentDto
    {
        public required string Name {  get; init; }
        public required TournamentType Type { get; init; }
        public required string GameType { get; init; }
        public required DateTime StartDate { get; init; }
        public required bool RandomnizeTeams { get; init; }
        public required IEnumerable<TeamDto> Teams { get; init; }
    }
}
