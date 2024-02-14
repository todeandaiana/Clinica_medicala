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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pacient>().ToTable("Pacienti");
            modelBuilder.Entity<Programare>().ToTable("Programari");
            modelBuilder.Entity<Serviciu>().ToTable("Servicii");
        }
    }
}

