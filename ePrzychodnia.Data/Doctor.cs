using System.Collections.Generic;
using ePrzychodnia.Core.Enums;

namespace ePrzychodnia.Data
{
    public class Doctor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public Specialization Specialization { get; set; }

        public List<Visit> Visits { get; set; }
    }
}