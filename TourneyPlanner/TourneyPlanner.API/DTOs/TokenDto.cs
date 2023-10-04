namespace TourneyPlanner.API.DTOs
{
    /// <summary>
    /// DTO for access token 
    /// </summary>
    public record struct TokenDto
    {
        public required string TokenString { get; init; }
        public required int ExpiresIn { get; init; }
    }
}
