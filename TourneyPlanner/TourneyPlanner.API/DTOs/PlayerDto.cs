namespace TourneyPlanner.API.DTOs
{
    public record struct PlayerDto
    {
        public required string Id { get; init; }
        public required string FirstName { get; init; }
        public required string LastName { get; init; }

    }
}
