namespace TourneyPlanner.API.DTOs
{
    public record struct MatchupDto
    {
        public required int Id { get; init; }
        public required int Round { get; init; }
        public int? WamId { get; init; }        
        public required IEnumerable<TeamDto> Teams { get; init; }
    }
}
