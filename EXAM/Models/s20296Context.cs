using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace kolokwium.Models
{
    public partial class s20296Context : DbContext
    {
        public s20296Context()
        {
        }

        public s20296Context(DbContextOptions<s20296Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Action> Actions { get; set; }
        public virtual DbSet<FireTruck> FireTrucks { get; set; }
        public virtual DbSet<FireTruckAction> FireTruckActions { get; set; }
        public virtual DbSet<Firefighter> Firefighters { get; set; }
        public virtual DbSet<FirefighterAction> FirefighterActions { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Polish_CI_AS");

            modelBuilder.Entity<Action>(entity =>
            {
                entity.HasKey(e => e.IdAction)
                    .HasName("Action_pk");

                entity.ToTable("Action");

                entity.Property(e => e.IdAction).ValueGeneratedNever();

                entity.Property(e => e.EndTime).HasColumnType("datetime");

                entity.Property(e => e.StartTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<FireTruck>(entity =>
            {
                entity.HasKey(e => e.IdFireTruck)
                    .HasName("FireTruck_pk");

                entity.ToTable("FireTruck");

                entity.Property(e => e.IdFireTruck).ValueGeneratedNever();

                entity.Property(e => e.OperationalNumber)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<FireTruckAction>(entity =>
            {
                entity.HasKey(e => e.IdFireTruckAction)
                    .HasName("FireTruck_Action_pk");

                entity.ToTable("FireTruck_Action");

                entity.Property(e => e.IdFireTruckAction).ValueGeneratedNever();

                entity.Property(e => e.AssignmentDate).HasColumnType("datetime");

                entity.HasOne(d => d.IdActionNavigation)
                    .WithMany(p => p.FireTruckActions)
                    .HasForeignKey(d => d.IdAction)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FireTruck_Action_Action");

                entity.HasOne(d => d.IdFireTruckNavigation)
                    .WithMany(p => p.FireTruckActions)
                    .HasForeignKey(d => d.IdFireTruck)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FireTruck_Action_FireTruck");
            });

            modelBuilder.Entity<Firefighter>(entity =>
            {
                entity.HasKey(e => e.IdFirefighter)
                    .HasName("Firefighter_pk");

                entity.ToTable("Firefighter");

                entity.Property(e => e.IdFirefighter).ValueGeneratedNever();

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<FirefighterAction>(entity =>
            {
                entity.HasKey(e => new { e.IdFirefighter, e.IdAction })
                    .HasName("Firefighter_Action_pk");

                entity.ToTable("Firefighter_Action");

                entity.HasOne(d => d.IdActionNavigation)
                    .WithMany(p => p.FirefighterActions)
                    .HasForeignKey(d => d.IdAction)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Firefighter_Action_Action");

                entity.HasOne(d => d.IdFirefighterNavigation)
                    .WithMany(p => p.FirefighterActions)
                    .HasForeignKey(d => d.IdFirefighter)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Firefighter_Action_Firefighter");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
