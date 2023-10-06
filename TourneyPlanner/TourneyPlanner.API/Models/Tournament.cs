using System;
using System.Collections.Generic;

namespace TourneyPlanner.API.Models;

public partial class Tournament
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public int GameTypeId { get; set; }

    public int TournamentTypeId { get; set; }

    public int UserId { get; set; }

    public virtual GameType GameType { get; set; } = null!;

    public virtual ICollection<Matchup> Matchups { get; set; } = new List<Matchup>();

    public virtual TournamentType TournamentType { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
