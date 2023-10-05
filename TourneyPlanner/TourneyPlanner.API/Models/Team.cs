using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TourneyPlanner.API.Models;

[Table("Team")]
public partial class Team
{
    [Key]
    [Column("TeamID")]
    public int TeamId { get; set; }

    [StringLength(30)]
    [Unicode(false)]
    public string TeamName { get; set; } = null!;

    [InverseProperty("Team")]
    public virtual ICollection<MatchupTeam> MatchupTeams { get; set; } = new List<MatchupTeam>();

    [InverseProperty("Team")]
    public virtual ICollection<Player> Players { get; set; } = new List<Player>();
}
