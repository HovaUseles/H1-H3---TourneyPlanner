namespace TourneyPlanner.API.DTOs
{
    public record struct MatchupChangeScoreDto
    {
        public required int TeamId { get; init; }
        public required int Score { get; init; }
    }
}
