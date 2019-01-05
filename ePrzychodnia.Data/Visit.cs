using System;

namespace ePrzychodnia.Data
{
    public class Visit
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }

        public int PatientId { get; set; }
        public Patient Patient { get; set; }

        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }

        public string Notes { get; set; }

    }
}