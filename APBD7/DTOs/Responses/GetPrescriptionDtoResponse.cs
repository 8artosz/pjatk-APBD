using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cw8.DTOs.Responses
{
    public class GetPrescriptionDtoResponse
    {
        public int IdPrescription { get; set; }
        public DateTime Date { get; set; }
        public DateTime DueDate { get; set; }
        public int IdPatient { get; set; }
        public string PatientFirstName { get; set; }
        public string PatientLastName { get; set; }
        public DateTime PatientBirthDate { get; set; }
        public int IdDoctor { get; set; }
        public string DoctorFirstName { get; set; }
        public string DoctorLastName { get; set; }
        public string DoctorEmail { get; set; }
        public IEnumerable<MedicamentDtoResponse> Medicaments { get; set; }


    }
}
