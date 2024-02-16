using System.Security.Policy;
using Clinica_medicala.Models.ClinicaViewModels;

namespace Clinica_medicala.Models.ClinicaViewModels
{
    public class MedicIndexData
    {
        public IEnumerable<Medic> Medici { get; set; }
        public IEnumerable<Serviciu> Servicii { get; set; }
        public IEnumerable<Programare> Programari { get; set; }

    }
}
