namespace TourneyPlanner.API.DTOs
{
    /// <summary>
    /// Ingoing DTO for logging in as a User
    /// </summary>
    public record struct LoginDto
    {
        public required string Username { get; init; }
        public required string Password { get; init; }
    }
}
