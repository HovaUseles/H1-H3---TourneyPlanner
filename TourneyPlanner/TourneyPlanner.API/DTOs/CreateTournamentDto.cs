using TourneyPlanner.API.Models;

namespace TourneyPlanner.API.DTOs
{
    public record struct CreateTournamentDto
    {
        public required string Name {  get; init; }
        public required int TournamentTypeId { get; init; }
        public required int GameTypeId { get; init; }
        public required DateTime StartDate { get; init; }
        public required bool RandomnizeTeams { get; init; }
        public required IEnumerable<TeamDto> Teams { get; init; }
    }
}
