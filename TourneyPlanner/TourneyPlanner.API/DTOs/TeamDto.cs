namespace TourneyPlanner.API.DTOs
{
    /// <summary>
    /// DTO for holding team data and score in a Matchup
    /// </summary>
    public record struct TeamDto
    {
        public required int Id { get; init; }
        public required string TeamName { get; init; }
        public required int Score { get; init; }
        public required IEnumerable<PlayerDto> Players { get; init; }
    }
}
