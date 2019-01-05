using System;

namespace ePrzychodnia.Core.ViewModels.Visit
{
    public class VisitViewModel
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }

        public int PatientId { get; set; }
        public string PatientName { get; set; }
        public string PatientSurname { get; set; }
        public string PatientPesel { get; set; }
        public string DoctorName { get; set; }
        public string DoctorSurname { get; set; }
        public int DoctorId { get; set; }
    }
}