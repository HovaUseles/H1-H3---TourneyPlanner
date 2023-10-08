namespace TourneyPlanner.API.DTOs
{

    /// <summary>
    /// In and Out DTO for User object
    /// </summary>
    public record struct UserDto
    {
        public required int Id { get; init; }
        public required string Email { get; init; }
    }
}
