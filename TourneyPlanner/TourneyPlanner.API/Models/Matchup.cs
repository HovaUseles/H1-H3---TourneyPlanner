using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TourneyPlanner.API.Models;

[Table("Matchup")]
public partial class Matchup
{
    [Key]
    [Column("MatchupID")]
    public int MatchupId { get; set; }

    public int Rounds { get; set; }

    [Column("TourneyID")]
    public int TourneyId { get; set; }

    public int? NextMatchupId { get; set; }

    [InverseProperty("NextMatchup")]
    public virtual ICollection<Matchup> InverseNextMatchup { get; set; } = new List<Matchup>();

    [InverseProperty("Matchup")]
    public virtual ICollection<MatchupTeam> MatchupTeams { get; set; } = new List<MatchupTeam>();

    [ForeignKey("NextMatchupId")]
    [InverseProperty("InverseNextMatchup")]
    public virtual Matchup? NextMatchup { get; set; }

    [ForeignKey("TourneyId")]
    [InverseProperty("Matchups")]
    public virtual Tournament Tourney { get; set; } = null!;
}
