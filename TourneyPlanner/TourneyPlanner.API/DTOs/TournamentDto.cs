using TourneyPlanner.API.Models;

namespace TourneyPlanner.API.DTOs
{
    /// <summary>
    /// In and outgoing DTO for Tournament
    /// </summary>
    public record struct TournamentDto
    {
        public required int Id { get; init; }
        public required TournamentTypes Type { get; init; }
        public required string GameType { get; init; }
        public required DateTime StartDate { get; init; }
        public required UserDto CreatedBy { get; init; }
        public required IEnumerable<MatchupDto> Matchups { get; init; }
    }

}
