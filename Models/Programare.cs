namespace Clinica_medicala.Models
{
    public class Programare
    {
        public int ProgramareID { get; set; }
        public int PacientID {  get; set; }
        public int ServiciuID {  get; set; }
        public Pacient Pacient { get; set; }
        public Serviciu Serviciu { get; set; }
    }
}
