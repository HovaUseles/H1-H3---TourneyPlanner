using System;
using System.Collections.Generic;

namespace TourneyPlanner.API.Models;

public partial class Matchup
{
    public int Id { get; set; }

    public int Rounds { get; set; }

    public int TourneyId { get; set; }

    public int? NextMatchupId { get; set; }

    public virtual ICollection<Matchup> InverseNextMatchup { get; set; } = new List<Matchup>();

    public virtual ICollection<MatchupTeam> MatchupTeams { get; set; } = new List<MatchupTeam>();

    public virtual Matchup? NextMatchup { get; set; }

    public virtual Tournament Tourney { get; set; } = null!;
}
