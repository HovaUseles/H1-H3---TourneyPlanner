using System;
using System.Collections.Generic;

namespace TourneyPlanner.API.Models;

public partial class GameType
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int TeamsPerMatch { get; set; }

    public int PointsForDraw { get; set; }

    public int PointsForWin { get; set; }

    public virtual ICollection<Tournament> Tournaments { get; set; } = new List<Tournament>();
}
