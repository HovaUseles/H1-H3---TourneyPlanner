using TourneyPlanner.API.Models;

namespace TourneyPlanner.API.DTOs
{
    public record struct CreateTournamentDto
    {
        public required TournamentTypes Type { get; init; }
        public required string GameType { get; init; }
        public required DateTime StartDate { get; init; }
        public required bool RandomnizeTeams { get; init; }
        public required IEnumerable<TeamDto> Teams { get; init; }
    }
}
