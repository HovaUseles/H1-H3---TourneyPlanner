using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TourneyPlanner.API.Models;

[Table("GameType")]
public partial class GameType
{
    [Key]
    public int GameTypeId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    public int TeamsPerMatch { get; set; }

    public int PointsForDraw { get; set; }

    public int PointsForWin { get; set; }

    [InverseProperty("GameType")]
    public virtual ICollection<Tournament> Tournaments { get; set; } = new List<Tournament>();
}
