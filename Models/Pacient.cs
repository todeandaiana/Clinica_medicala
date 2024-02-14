namespace Clinica_medicala.Models
{
    public class Pacient
    {
        public int PacientID { get; set; }
        public string Nume { get; set; }
        public string Adresa { get; set; }
        public DateTime DataNasterii { get; set; }
        public ICollection<Programare> Programari { get; set; }

    }
}
