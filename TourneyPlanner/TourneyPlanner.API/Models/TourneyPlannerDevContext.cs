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
        => optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=TourneyPlannerDev;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<FavoritMatchup>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__FavoritM__3214EC07E9748BFD");

            entity.ToTable("FavoritMatchup");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Matchup).WithMany(p => p.FavoritMatchups)
                .HasForeignKey(d => d.MatchupId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__FavoritMa__Match__4D94879B");

            entity.HasOne(d => d.User).WithMany(p => p.FavoritMatchups)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__FavoritMa__UserI__4E88ABD4");
        });

        modelBuilder.Entity<GameType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__GameType__3214EC079EC4722B");

            entity.ToTable("GameType");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Matchup>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Matchup__3214EC078DA3DA6F");

            entity.ToTable("Matchup");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.StartDateTime).HasColumnType("datetime");

            entity.HasOne(d => d.NextMatchup).WithMany(p => p.InverseNextMatchup)
                .HasForeignKey(d => d.NextMatchupId)
                .HasConstraintName("FK__Matchup__NextMat__412EB0B6");

            entity.HasOne(d => d.Tournament).WithMany(p => p.Matchups)
                .HasForeignKey(d => d.TournamentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Matchup__Tournam__4222D4EF");
        });

        modelBuilder.Entity<MatchupTeam>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__MatchupT__3214EC07DC389E4A");

            entity.ToTable("MatchupTeam");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Matchup).WithMany(p => p.MatchupTeams)
                .HasForeignKey(d => d.MatchupId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__MatchupTe__Match__4AB81AF0");

            entity.HasOne(d => d.Team).WithMany(p => p.MatchupTeams)
                .HasForeignKey(d => d.TeamId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__MatchupTe__TeamI__49C3F6B7");
        });

        modelBuilder.Entity<Player>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Player__3214EC073E1A5FE2");

            entity.ToTable("Player");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Team).WithMany(p => p.Players)
                .HasForeignKey(d => d.TeamId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Player__TeamId__46E78A0C");
        });

        modelBuilder.Entity<Team>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Team__3214EC07FB70BA78");

            entity.ToTable("Team");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Tournament>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Tourname__3214EC071CA8E474");

            entity.ToTable("Tournament");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.EndDate).HasColumnType("date");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.StartDate).HasColumnType("date");

            entity.HasOne(d => d.GameType).WithMany(p => p.Tournaments)
                .HasForeignKey(d => d.GameTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Tournamen__GameT__3D5E1FD2");

            entity.HasOne(d => d.TournamentType).WithMany(p => p.Tournaments)
                .HasForeignKey(d => d.TournamentTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Tournamen__Tourn__3E52440B");

            entity.HasOne(d => d.User).WithMany(p => p.Tournaments)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Tournamen__UserI__3C69FB99");
        });

        modelBuilder.Entity<TournamentType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Tourname__3214EC07E96930BA");

            entity.ToTable("TournamentType");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__User__3214EC073F6BF9BA");

            entity.ToTable("User");

            entity.Property(e => e.Id).ValueGeneratedNever();
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
