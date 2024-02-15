using System.ComponentModel.DataAnnotations.Schema;

namespace Clinica_medicala.Models
{
    public class Serviciu
    {
        public int ServiciuID { get; set; }
        public string Titlu { get; set; }
        public int SpecializareID { get; set; }
        public Specializare Specializare { get; set; }

        [Column(TypeName = "decimal(6, 2)")]
        public decimal Pret { get; set; }

        public ICollection<Programare>? Programari { get; set; }

        public ICollection<ServiciuPrestat>? ServiciiPrestate { get; set; }
    }
}
