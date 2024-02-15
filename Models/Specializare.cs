namespace Clinica_medicala.Models
{
    public class Specializare
    {
        public int SpecializareID { get; set; }
        public string Nume { get; set; }

        public ICollection<Serviciu> Servicii { get; set; }
    }
}
