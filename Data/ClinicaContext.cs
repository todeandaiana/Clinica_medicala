using Microsoft.EntityFrameworkCore;
using Clinica_medicala.Models;

namespace Clinica_medicala.Data
{
    public class ClinicaContext:DbContext
        {
            public LibraryContext(DbContextOptions<LibraryContext> options) :
           base(options)
            {
            }
            public DbSet<Customer> Customers { get; set; }
            public DbSet<Order> Orders { get; set; }
            public DbSet<Book> Books { get; set; }
        }
    }
}
