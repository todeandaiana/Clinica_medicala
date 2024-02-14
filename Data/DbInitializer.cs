using Microsoft.EntityFrameworkCore;
using Clinica_medicala.Models;

namespace Clinica_medicala.Data
{
    public class DbInitializer
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ClinicaContext(serviceProvider.GetRequiredService<DbContextOptions<ClinicaContext>>()))
            {
                if (context.Servicii.Any())
                {
                    return; // BD a fost creata anterior
                }
                context.Servicii.AddRange(
                new Serviciu
                {
                    Titlu = "Consult dermatologic",
                    Medic = "Muresan Teodora",
                    Pret=Decimal.Parse("220")},
               
                new Serviciu
                {
                    Titlu = "Control dermatologic",
                    Medic = "Pop Larisa",
                    Pret = Decimal.Parse("180")
                },

                new Serviciu
                {
                    Titlu = "Peeling chimic",
                    Medic = "Chira Denisa",
                    Pret = Decimal.Parse("300")
                }


                );


                context.Pacienti.AddRange(
                new Pacient
                {
                    Nume = "Pantea Paula",
                    Adresa = "Str. Aurora, nr. 6",
                    DataNasterii = DateTime.Parse("1999-07-15")
                },
                new Pacient
                {
                    Nume = "Popescu Adriana",
                    Adresa = "Str. Lalelelor, nr. 24",
                    DataNasterii = DateTime.Parse("1998-09-12")
                }

                );

                context.SaveChanges();
            }
        }
    }
}
