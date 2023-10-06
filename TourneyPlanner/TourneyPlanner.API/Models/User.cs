using System;
using System.Collections.Generic;

namespace TourneyPlanner.API.Models;

public partial class User
{
    public int Id { get; set; }

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string Salt { get; set; } = null!;

    public virtual ICollection<FavoritMatchup> FavoritMatchups { get; set; } = new List<FavoritMatchup>();

    public virtual ICollection<Tournament> Tournaments { get; set; } = new List<Tournament>();
}
