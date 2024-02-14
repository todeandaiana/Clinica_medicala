namespace Clinica_medicala.Models
{
    public class Serviciu
    {
        public int ID { get; set; }
        public string Titlu { get; set; }
        public string Medic { get; set; }
        public decimal Pret { get; set; }

       // public int? ProgramID { get; set; }
        //public Program Program { get; set; }
        public ICollection<Programare> Programari { get; set; }
    }
}
