using System.ComponentModel.DataAnnotations;

namespace Clinica_medicala.Models.ClinicaViewModels
{
    public class GrupProgramare
    {
        [DataType(DataType.Date)]
        public DateTime? DataProgramare { get; set; }
        public int NrServicii { get; set; }

    }
}
