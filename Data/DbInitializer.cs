using Microsoft.EntityFrameworkCore;
using Clinica_medicala.Models;
using System.Security.Policy;

namespace Clinica_medicala.Data
{
    public class DbInitializer
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ClinicaContext(serviceProvider.GetRequiredService<DbContextOptions<ClinicaContext>>()))
            {
                /* if (context.Servicii.Any())
                 {
                     return; // BD a fost creata anterior
                 }
                */

                /*   var programari = new Programare[]
                   {
                   new Programare{ServiciuID=1,PacientID=1,DataProgramare=DateTime.Parse("2021-02-25")},
                   new Programare{ServiciuID=3,PacientID=1,DataProgramare=DateTime.Parse("2021-09-28")},
                   new Programare{ServiciuID=1,PacientID=1,DataProgramare=DateTime.Parse("2021-10-28")},
                   new Programare{ServiciuID=2,PacientID=5,DataProgramare=DateTime.Parse("2021-09-28")},
                   new Programare{ServiciuID=4,PacientID=7,DataProgramare=DateTime.Parse("2021-09-28")},
                   new Programare{ServiciuID=6,PacientID=12,DataProgramare=DateTime.Parse("2021-10-28")},
                   };
                  foreach (Programare e in programari)
                  {
                      context.Programari.Add(e);
                  }
                  context.SaveChanges();

                 var medici = new Medic[]
                  {

                   new Medic{Nume="Sirbu Cristian"},
                   new Medic{Nume="Salatioan Andra"},
                   new Medic{Nume="Fechete Laura"},
                   new Medic{Nume="Berei Bogdan"}
                   };
                  foreach (Medic p in medici)
                  {
                      context.Medici.Add(p);
                  }
                  //context.SaveChanges();

                  var servicii = context.Servicii;
                  var serviciiprestate = new ServiciuPrestat[]
                  {
                  new ServiciuPrestat
                      {
                      ServiciuID = servicii.Single(c => c.Titlu == "Consult dermatologie" ).ServiciuID,
                      MedicID = medici.Single(i => i.Nume =="Fechete Laura").MedicID
                      },

                  new ServiciuPrestat
                      {
                      ServiciuID = servicii.Single(c => c.Titlu == "Consult endocrinologie" ).ServiciuID,
                      MedicID = medici.Single(i => i.Nume =="Berei Bogdan").MedicID
                      },

                  new ServiciuPrestat
                      {
                      ServiciuID = servicii.Single(c => c.Titlu == "Peeling chimic" ).ServiciuID,
                      MedicID = medici.Single(i => i.Nume =="Sirbu Cristian").MedicID
                      },

                  new ServiciuPrestat
                      {
                      ServiciuID = servicii.Single(c => c.Titlu == "RMN" ).ServiciuID,
                      MedicID = medici.Single(i => i.Nume =="Salatioan Andra").MedicID
                      }
                  };

                  foreach (ServiciuPrestat pb in serviciiprestate)
                  {
                      context.ServiciiPrestate.Add(pb);
                  }
                  context.SaveChanges();
                 */
            }
        }






        /* context.Servicii.AddRange(
         new Serviciu
         {
             Titlu = "Consult dermatologic",
             Pret=Decimal.Parse("220")},

         new Serviciu
         {
             Titlu = "Control dermatologic",
             Pret = Decimal.Parse("180")
         },

         new Serviciu
         {
             Titlu = "Peeling chimic",
             Pret = Decimal.Parse("300")
         },

         new Serviciu
         {
             Titlu = "Consult endocrinologie",
             Pret = Decimal.Parse("200")
         },

         new Serviciu
         {
             Titlu = "Control endocrinologie",
             Pret = Decimal.Parse("150")
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
         },

          new Pacient
          {
              Nume = "Morar Cristian",
              Adresa = "Str. Fagului, nr. 2",
              DataNasterii = DateTime.Parse("1992-10-11")
          },

           new Pacient
           {
               Nume = "Dragan Livia",
               Adresa = "Str. Venus, nr. 15",
               DataNasterii = DateTime.Parse("1994-01-25")
           }

         ); 

         context.SaveChanges();
        */
    }
}
    

