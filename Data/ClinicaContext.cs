using Microsoft.EntityFrameworkCore;
using Clinica_medicala.Models;

namespace Clinica_medicala.Data
{
    public class ClinicaContext:DbContext
        {
            public ClinicaContext(DbContextOptions<ClinicaContext> options) :
           base(options)
            {
            }
            public DbSet<Pacient> Pacienti { get; set; }
            public DbSet<Programare> Programari { get; set; }
            public DbSet<Serviciu> Servicii { get; set; }
            public DbSet<Medic> Medici { get; set; }
            public DbSet<ServiciuPrestat> ServiciiPrestate { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pacient>().ToTable("Pacienti");
            modelBuilder.Entity<Programare>().ToTable("Programari");
            modelBuilder.Entity<Serviciu>().ToTable("Servicii");
            modelBuilder.Entity<Medic>().ToTable("Medici");
            modelBuilder.Entity<ServiciuPrestat>().ToTable("ServiciiPrestate");

            modelBuilder.Entity<ServiciuPrestat>().HasKey(c => new { c.ServiciuID, c.MedicID });


        }
    }
}

