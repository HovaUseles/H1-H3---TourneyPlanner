using System;
using System.Collections.Generic;

namespace TourneyPlanner.API.Models;

public partial class FavoritMatchup
{
    public int Id { get; set; }

    public int MatchupId { get; set; }

    public int UserId { get; set; }

    public virtual Matchup Matchup { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
