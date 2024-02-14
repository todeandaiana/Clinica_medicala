namespace Clinica_medicala.Models
{
    public class Serviciu
    {
        public int ServiciuID { get; set; }
        public string Titlu { get; set; }
        public string Medic { get; set; }
        public decimal Pret { get; set; }

       // public int? ProgramID { get; set; }
        //public Program Program { get; set; }
        public ICollection<Programare> Programari { get; set; }

        public ICollection<ServiciuPrestat> ServiciiPrestate { get; set; }
    }
}
