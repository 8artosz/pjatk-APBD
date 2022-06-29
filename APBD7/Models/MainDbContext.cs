using cw8.Models;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cw8
{
    public class MainDbContext : DbContext
    {
        public MainDbContext()
        {

        }
        public MainDbContext(DbContextOptions options) : base (options)
        {

        }

        public DbSet<Doctor> Doctor { get; set; }
        public DbSet<Prescription> Prescription { get; set; }
        public DbSet<Patient> Patient { get; set; }
        public DbSet<PrescriptionMedicament> PrescriptionMedicament { get; set; }
        public DbSet<Medicament> Medicament { get; set; }
        public DbSet<AppUser> User { get; set; }


        protected override void OnModelCreating (ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Doctor>(opt =>
            {
                opt.HasKey(e => e.IdDoctor);
                opt.Property(e => e.IdDoctor).ValueGeneratedOnAdd();
                opt.Property(e => e.FirstName).IsRequired().HasMaxLength(100);
                opt.Property(e => e.LastName).IsRequired().HasMaxLength(100);
                opt.Property(e => e.Email).IsRequired().HasMaxLength(100);

                opt.HasData(
                    new Doctor { IdDoctor = 1, FirstName = "Bartosz", LastName = "Wasilewski", Email = "bartoszw@gmail.com" },
                    new Doctor { IdDoctor = 2, FirstName = "Jan", LastName = "Kowalski", Email = "jank@gmail.com" },
                    new Doctor { IdDoctor = 3, FirstName = "Anna", LastName = "Nowak", Email = "annan@gmail.com" }
                    );

            });
            modelBuilder.Entity<Prescription>(opt =>
            {
                
                opt.HasKey(e => e.IdPrescription);
                opt.Property(e => e.IdPrescription).ValueGeneratedOnAdd();
                opt.Property(e => e.Date).IsRequired();
                opt.Property(e => e.DueDate).IsRequired();

                opt.HasOne(d => d.Doctor)
                    .WithMany(p => p.Prescriptions)
                    .HasForeignKey(p => p.IdDoctor);

                opt.HasOne(p => p.Patient)
                    .WithMany(pr => pr.Prescriptions)
                    .HasForeignKey(pr => pr.IdPatient);

                opt.HasData(
                    new Prescription { IdPrescription = 1, Date = DateTime.Parse("29/04/2021"), DueDate = DateTime.Parse("06/05/2021"), IdPatient = 1, IdDoctor = 1 },
                    new Prescription { IdPrescription = 2, Date = DateTime.Parse("29/04/2021"), DueDate = DateTime.Parse("06/05/2021"), IdPatient = 2, IdDoctor = 1 },
                    new Prescription { IdPrescription = 3, Date = DateTime.Parse("29/04/2021"), DueDate = DateTime.Parse("06/05/2021"), IdPatient = 3, IdDoctor = 2 }

                     );

            });

            modelBuilder.Entity<Patient>(opt =>
            {
                
                opt.HasKey(e => e.IdPatient);
                opt.Property(e => e.IdPatient).ValueGeneratedOnAdd();
                opt.Property(e => e.FirstName).IsRequired().HasMaxLength(100);
                opt.Property(e => e.LastName).IsRequired().HasMaxLength(100);
                opt.Property(e => e.BirthDate).IsRequired();

                opt.HasData(
                    new Patient { IdPatient = 1, FirstName = "Ola", LastName = "Nowak", BirthDate = DateTime.Parse("29/04/2021") },
                    new Patient { IdPatient = 2, FirstName = "Rafal", LastName = "Kowalewski", BirthDate = DateTime.Parse("29/04/2021") },
                    new Patient { IdPatient = 3, FirstName = "Kuba", LastName = "Nowakowska", BirthDate = DateTime.Parse("29/04/2021") }
                    ); ;

            });

            modelBuilder.Entity<Medicament>(opt =>
            {
                
                opt.HasKey(e => e.IdMedicament);
                opt.Property(e => e.IdMedicament).ValueGeneratedOnAdd();
                opt.Property(e => e.Name).IsRequired().HasMaxLength(100);
                opt.Property(e => e.Description).IsRequired().HasMaxLength(100);
                opt.Property(e => e.Type).IsRequired().HasMaxLength(100);

                opt.HasData(
                    new Medicament { IdMedicament = 1, Name = "Strepsils", Description = "Used to relieve discomfort caused by mouth and throat infections", Type = "throat lozenges"  },
                    new Medicament { IdMedicament = 2, Name = "Cholinex", Description = "Used to relieve discomfort caused by mouth and throat infections", Type = "throat lozenges"  },
                    new Medicament { IdMedicament = 3, Name = "Neo-Angin", Description = "Used to relieve discomfort caused by mouth and throat infections", Type = "throat lozenges"  }
                    ); ;

            });
            modelBuilder.Entity<PrescriptionMedicament>(opt =>
            {
                opt.ToTable("Prescription_Medicament");
                opt.HasKey(e => new { e.IdMedicament, e.IdPrescription });
                opt.Property(e => e.Details).IsRequired().HasMaxLength(100);

                opt.HasData(
                    new PrescriptionMedicament { IdMedicament = 1, IdPrescription = 1, Dose = 2, Details = "Use once a day"},
                    new PrescriptionMedicament { IdMedicament = 3, IdPrescription = 1, Details = "Use twice a day" },
                    new PrescriptionMedicament { IdMedicament = 2, IdPrescription = 3, Dose = 1, Details = "Use once a day" }

                    );

                opt.HasOne(d => d.Medicament)
                    .WithMany(p => p.PrescriptionMedicaments)
                    .HasForeignKey(p => p.IdMedicament);

                opt.HasOne(p => p.Prescription)
                    .WithMany(pr => pr.PrescriptionMedicaments)
                    .HasForeignKey(pr => pr.IdPrescription);

            });
            modelBuilder.Entity<AppUser>(opt =>
            {
                opt.HasKey(e => e.IdUser);
                opt.Property(e => e.IdUser).ValueGeneratedOnAdd();

            });


        }
    }
}
