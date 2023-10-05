using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TourneyPlanner.API.Models;

[Table("MatchupTeam")]
public partial class MatchupTeam
{
    [Key]
    [Column("MatchupTeamID")]
    public int MatchupTeamId { get; set; }

    [Column("TeamID")]
    public int TeamId { get; set; }

    [Column("MatchupID")]
    public int MatchupId { get; set; }

    public int? Score { get; set; }

    [ForeignKey("MatchupId")]
    [InverseProperty("MatchupTeams")]
    public virtual Matchup Matchup { get; set; } = null!;

    [ForeignKey("TeamId")]
    [InverseProperty("MatchupTeams")]
    public virtual Team Team { get; set; } = null!;
}
