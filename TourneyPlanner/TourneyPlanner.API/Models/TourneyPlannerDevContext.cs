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
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=TourneyPlannerDev;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<GameType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__GameType__B5BCC1F62BC4D9C5");

            entity.ToTable("GameType");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Matchup>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Matchup__DE3C356933C31D8F");

            entity.ToTable("Matchup");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("MatchupID");
            entity.Property(e => e.TourneyId).HasColumnName("TourneyID");

            entity.HasOne(d => d.NextMatchup).WithMany(p => p.InverseNextMatchup)
                .HasForeignKey(d => d.NextMatchupId)
                .HasConstraintName("FK__Matchup__NextMat__2F10007B");

            entity.HasOne(d => d.Tourney).WithMany(p => p.Matchups)
                .HasForeignKey(d => d.TourneyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Matchup__Tourney__300424B4");
        });

        modelBuilder.Entity<MatchupTeam>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__MatchupT__4C3709675B117F97");

            entity.ToTable("MatchupTeam");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("MatchupTeamID");
            entity.Property(e => e.MatchupId).HasColumnName("MatchupID");
            entity.Property(e => e.TeamId).HasColumnName("TeamID");

            entity.HasOne(d => d.Matchup).WithMany(p => p.MatchupTeams)
                .HasForeignKey(d => d.MatchupId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__MatchupTe__Match__38996AB5");

            entity.HasOne(d => d.Team).WithMany(p => p.MatchupTeams)
                .HasForeignKey(d => d.TeamId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__MatchupTe__TeamI__37A5467C");
        });

        modelBuilder.Entity<Player>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Player__4A4E74A8747A6F3F");

            entity.ToTable("Player");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("PlayerID");
            entity.Property(e => e.FirstName)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.TeamId).HasColumnName("TeamID");

            entity.HasOne(d => d.Team).WithMany(p => p.Players)
                .HasForeignKey(d => d.TeamId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Player__TeamID__34C8D9D1");
        });

        modelBuilder.Entity<Team>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Team__123AE7B9200EF20B");

            entity.ToTable("Team");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("TeamID");
            entity.Property(e => e.TeamName)
                .HasMaxLength(30)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Tournament>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Tourname__420410E7F54B3A87");

            entity.ToTable("Tournament");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("TourneyID");
            entity.Property(e => e.StartDate).HasColumnType("date");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.GameType).WithMany(p => p.Tournaments)
                .HasForeignKey(d => d.GameTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Tournamen__GameT__2B3F6F97");

            entity.HasOne(d => d.Type).WithMany(p => p.Tournaments)
                .HasForeignKey(d => d.TypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Tournamen__TypeI__2C3393D0");

            entity.HasOne(d => d.User).WithMany(p => p.Tournaments)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Tournamen__UserI__2A4B4B5E");
        });

        modelBuilder.Entity<TournamentType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Tourname__766E51B8B870BCA1");

            entity.ToTable("TournamentType");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__1788CCACFDB9BBBE");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("UserID");
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Salt)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(30)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
