using System;
using System.Collections.Generic;

namespace TourneyPlanner.API.Models;

public partial class TournamentType
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Tournament> Tournaments { get; set; } = new List<Tournament>();
}
