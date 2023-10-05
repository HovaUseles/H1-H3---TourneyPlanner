using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TourneyPlanner.API.Models;

public partial class TourneyPlannerDevContext : DbContext
{
    public TourneyPlannerDevContext()
    {
    }

    public TourneyPlannerDevContext(DbContextOptions<TourneyPlannerDevContext> options)
        : base(options)
    {
    }

    public virtual DbSet<GameType> GameTypes { get; set; }

    public virtual DbSet<Matchup> Matchups { get; set; }

    public virtual DbSet<MatchupTeam> MatchupTeams { get; set; }

    public virtual DbSet<Player> Players { get; set; }

    public virtual DbSet<Team> Teams { get; set; }

    public virtual DbSet<Tournament> Tournaments { get; set; }

    public virtual DbSet<TournamentType> TournamentTypes { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:TourneyPlanner");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<GameType>(entity =>
        {
            entity.HasKey(e => e.GameTypeId).HasName("PK__GameType__B5BCC1F66DE1094C");

            entity.Property(e => e.GameTypeId).ValueGeneratedNever();
        });

        modelBuilder.Entity<Matchup>(entity =>
        {
            entity.HasKey(e => e.MatchupId).HasName("PK__Matchup__DE3C3569AB69ADBC");

            entity.Property(e => e.MatchupId).ValueGeneratedNever();

            entity.HasOne(d => d.NextMatchup).WithMany(p => p.InverseNextMatchup).HasConstraintName("FK__Matchup__NextMat__5EBF139D");

            entity.HasOne(d => d.Tourney).WithMany(p => p.Matchups)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Matchup__Tourney__5FB337D6");
        });

        modelBuilder.Entity<MatchupTeam>(entity =>
        {
            entity.HasKey(e => e.MatchupTeamId).HasName("PK__MatchupT__4C370967CC719346");

            entity.Property(e => e.MatchupTeamId).ValueGeneratedNever();

            entity.HasOne(d => d.Matchup).WithMany(p => p.MatchupTeams)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__MatchupTe__Match__68487DD7");

            entity.HasOne(d => d.Team).WithMany(p => p.MatchupTeams)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__MatchupTe__TeamI__6754599E");
        });

        modelBuilder.Entity<Player>(entity =>
        {
            entity.HasKey(e => e.PlayerId).HasName("PK__Player__4A4E74A8E7EBA336");

            entity.Property(e => e.PlayerId).ValueGeneratedNever();

            entity.HasOne(d => d.Team).WithMany(p => p.Players)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Player__TeamID__6477ECF3");
        });

        modelBuilder.Entity<Team>(entity =>
        {
            entity.HasKey(e => e.TeamId).HasName("PK__Team__123AE7B920299231");

            entity.Property(e => e.TeamId).ValueGeneratedNever();
        });

        modelBuilder.Entity<Tournament>(entity =>
        {
            entity.HasKey(e => e.TourneyId).HasName("PK__Tourname__420410E7BCE524AF");

            entity.Property(e => e.TourneyId).ValueGeneratedNever();

            entity.HasOne(d => d.GameType).WithMany(p => p.Tournaments)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Tournamen__GameT__5AEE82B9");

            entity.HasOne(d => d.Type).WithMany(p => p.Tournaments)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Tournamen__TypeI__5BE2A6F2");

            entity.HasOne(d => d.User).WithMany(p => p.Tournaments)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Tournamen__UserI__59FA5E80");
        });

        modelBuilder.Entity<TournamentType>(entity =>
        {
            entity.HasKey(e => e.TournamentTypeId).HasName("PK__Tourname__766E51B8B57A300F");

            entity.Property(e => e.TournamentTypeId).ValueGeneratedNever();
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CCAC1CE9A6F9");

            entity.Property(e => e.UserId).ValueGeneratedNever();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
