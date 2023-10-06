using System;
using System.Collections.Generic;

namespace TourneyPlanner.API.Models;

public partial class Team
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<MatchupTeam> MatchupTeams { get; set; } = new List<MatchupTeam>();

    public virtual ICollection<Player> Players { get; set; } = new List<Player>();
}
