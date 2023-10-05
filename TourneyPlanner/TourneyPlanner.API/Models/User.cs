using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TourneyPlanner.API.Models;

public partial class User
{
    [Key]
    [Column("UserID")]
    public int UserId { get; set; }

    [StringLength(30)]
    [Unicode(false)]
    public string Email { get; set; } = null!;

    [StringLength(30)]
    [Unicode(false)]
    public string PasswordHash { get; set; } = null!;

    [StringLength(30)]
    [Unicode(false)]
    public string Salt { get; set; } = null!;

    [InverseProperty("User")]
    public virtual ICollection<Tournament> Tournaments { get; set; } = new List<Tournament>();
}
