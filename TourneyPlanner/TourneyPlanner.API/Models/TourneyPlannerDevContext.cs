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

    public virtual DbSet<FavoritMatchup> FavoritMatchups { get; set; }

    public virtual DbSet<GameType> GameTypes { get; set; }

    public virtual DbSet<Matchup> Matchups { get; set; }

    public virtual DbSet<MatchupTeam> MatchupTeams { get; set; }

    public virtual DbSet<Player> Players { get; set; }

    public virtual DbSet<Team> Teams { get; set; }

    public virtual DbSet<Tournament> Tournaments { get; set; }

    public virtual DbSet<TournamentType> TournamentTypes { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Initial Catalog=TourneyPlannerDev;Trusted_Connection=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<FavoritMatchup>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__FavoritM__3214EC072EC0D6CD");

            entity.ToTable("FavoritMatchup");

            entity.HasOne(d => d.Matchup).WithMany(p => p.FavoritMatchups)
                .HasForeignKey(d => d.MatchupId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__FavoritMa__Match__5AB9788F");

            entity.HasOne(d => d.User).WithMany(p => p.FavoritMatchups)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__FavoritMa__UserI__5BAD9CC8");
        });

        modelBuilder.Entity<GameType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__GameType__3214EC07FA0F5D49");

            entity.ToTable("GameType");

            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Matchup>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Matchup__3214EC07B6E53B3B");

            entity.ToTable("Matchup");

            entity.Property(e => e.StartDateTime).HasColumnType("datetime");

            entity.HasOne(d => d.NextMatchup).WithMany(p => p.InverseNextMatchup)
                .HasForeignKey(d => d.NextMatchupId)
                .HasConstraintName("FK__Matchup__NextMat__4E53A1AA");

            entity.HasOne(d => d.Tournament).WithMany(p => p.Matchups)
                .HasForeignKey(d => d.TournamentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Matchup__Tournam__4F47C5E3");
        });

        modelBuilder.Entity<MatchupTeam>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__MatchupT__3214EC07FD245C91");

            entity.ToTable("MatchupTeam");

            entity.HasOne(d => d.Matchup).WithMany(p => p.MatchupTeams)
                .HasForeignKey(d => d.MatchupId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__MatchupTe__Match__57DD0BE4");

            entity.HasOne(d => d.Team).WithMany(p => p.MatchupTeams)
                .HasForeignKey(d => d.TeamId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__MatchupTe__TeamI__56E8E7AB");
        });

        modelBuilder.Entity<Player>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Player__3214EC0780D633E2");

            entity.ToTable("Player");

            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Team).WithMany(p => p.Players)
                .HasForeignKey(d => d.TeamId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Player__TeamId__540C7B00");
        });

        modelBuilder.Entity<Team>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Team__3214EC072B9A2B72");

            entity.ToTable("Team");

            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Tournament>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Tourname__3214EC07E89D09A6");

            entity.ToTable("Tournament");

            entity.Property(e => e.EndDate).HasColumnType("date");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.StartDate).HasColumnType("date");

            entity.HasOne(d => d.GameType).WithMany(p => p.Tournaments)
                .HasForeignKey(d => d.GameTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Tournamen__GameT__4A8310C6");

            entity.HasOne(d => d.TournamentType).WithMany(p => p.Tournaments)
                .HasForeignKey(d => d.TournamentTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Tournamen__Tourn__4B7734FF");

            entity.HasOne(d => d.User).WithMany(p => p.Tournaments)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Tournamen__UserI__498EEC8D");
        });

        modelBuilder.Entity<TournamentType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Tourname__3214EC071A7367C1");

            entity.ToTable("TournamentType");

            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__User__3214EC07D008A426");

            entity.ToTable("User");

            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Salt)
                .HasMaxLength(32)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
