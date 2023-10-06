using System;
using System.Collections.Generic;

namespace TourneyPlanner.API.Models;

public partial class Tournament
{
    public int Id { get; set; }

    public int GameTypeId { get; set; }

    public int UserId { get; set; }

    public int TypeId { get; set; }

    public DateTime StartDate { get; set; }

    public virtual GameType GameType { get; set; } = null!;

    public virtual ICollection<Matchup> Matchups { get; set; } = new List<Matchup>();

    public virtual TournamentType Type { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
