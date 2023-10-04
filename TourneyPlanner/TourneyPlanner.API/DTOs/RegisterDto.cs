namespace TourneyPlanner.API.DTOs
{
    /// <summary>
    /// Ingoing DTO for regsitering a new user
    /// </summary>
    public record struct RegisterDto
    {
        public required string Username { get; init; }
        public required string Password { get; init; }
    }
}
