using System.Security.Policy;

namespace Clinica_medicala.Models
{
    public class ServiciuPrestat
    {
        public int MedicID { get; set; }
        public int ServiciuID { get; set; }
        public Medic Medic { get; set; }
        public Serviciu Serviciu { get; set; }

    }
}
