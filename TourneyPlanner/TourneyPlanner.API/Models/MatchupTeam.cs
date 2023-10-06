using System;
using System.Collections.Generic;

namespace TourneyPlanner.API.Models;

public partial class MatchupTeam
{
    public int Id { get; set; }

    public int TeamId { get; set; }

    public int MatchupId { get; set; }

    public int? Score { get; set; }

    public virtual Matchup Matchup { get; set; } = null!;

    public virtual Team Team { get; set; } = null!;
}
