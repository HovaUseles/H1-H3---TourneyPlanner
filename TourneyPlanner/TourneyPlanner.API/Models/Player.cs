using System;
using System.Collections.Generic;

namespace TourneyPlanner.API.Models;

public partial class Player
{
    public int Id { get; set; }

    public int TeamId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public virtual Team Team { get; set; } = null!;
}
