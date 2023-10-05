﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TourneyPlanner.API.Models;

[Table("Player")]
public partial class Player
{
    [Key]
    [Column("PlayerID")]
    public int PlayerId { get; set; }

    [Column("TeamID")]
    public int TeamId { get; set; }

    [StringLength(30)]
    [Unicode(false)]
    public string FirstName { get; set; } = null!;

    [StringLength(30)]
    [Unicode(false)]
    public string LastName { get; set; } = null!;

    [ForeignKey("TeamId")]
    [InverseProperty("Players")]
    public virtual Team Team { get; set; } = null!;
}
