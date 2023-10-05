using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TourneyPlanner.API.Models;

[Table("Tournament")]
public partial class Tournament
{
    [Key]
    [Column("TourneyID")]
    public int TourneyId { get; set; }

    public int GameTypeId { get; set; }

    [Column("UserID")]
    public int UserId { get; set; }

    public int TypeId { get; set; }

    [Column(TypeName = "date")]
    public DateTime StartDate { get; set; }

    [ForeignKey("GameTypeId")]
    [InverseProperty("Tournaments")]
    public virtual GameType GameType { get; set; } = null!;

    [InverseProperty("Tourney")]
    public virtual ICollection<Matchup> Matchups { get; set; } = new List<Matchup>();

    [ForeignKey("TypeId")]
    [InverseProperty("Tournaments")]
    public virtual TournamentType Type { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("Tournaments")]
    public virtual User User { get; set; } = null!;
}
