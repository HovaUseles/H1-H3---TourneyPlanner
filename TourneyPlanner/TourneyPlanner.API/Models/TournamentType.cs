using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TourneyPlanner.API.Models;

[Table("TournamentType")]
public partial class TournamentType
{
    [Key]
    public int TournamentTypeId { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    [InverseProperty("Type")]
    public virtual ICollection<Tournament> Tournaments { get; set; } = new List<Tournament>();
}
