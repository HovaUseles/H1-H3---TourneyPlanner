using System;
using System.Collections.Generic;

namespace TourneyPlanner.API.Models;

public partial class Matchup
{
    public int Id { get; set; }

    public DateTime StartDateTime { get; set; }

    public int Rounds { get; set; }

    public int TournamentId { get; set; }

    public int? NextMatchupId { get; set; }

    public virtual ICollection<FavoritMatchup> FavoritMatchups { get; set; } = new List<FavoritMatchup>();

    public virtual ICollection<Matchup> InverseNextMatchup { get; set; } = new List<Matchup>();

    public virtual ICollection<MatchupTeam> MatchupTeams { get; set; } = new List<MatchupTeam>();

    public virtual Matchup? NextMatchup { get; set; }

    public virtual Tournament Tournament { get; set; } = null!;
}
