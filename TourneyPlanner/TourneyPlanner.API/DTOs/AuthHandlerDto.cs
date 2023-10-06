namespace TourneyPlanner.API.DTOs
{
    /// <summary>
    /// Ingoing DTO for regsitering a new user
    /// </summary>
    public record struct AuthHandlerDto
    {
        public required string Email { get; init; }
        public required string Password { get; init; }
    }
}
