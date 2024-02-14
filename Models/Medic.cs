namespace Clinica_medicala.Models
{
    public class Medic
    {
        public int MedicID { get; set; }
        public string Nume { get; set; }

        public ICollection<ServiciuPrestat> ServiciiPrestate { get; set; }

    }
}
