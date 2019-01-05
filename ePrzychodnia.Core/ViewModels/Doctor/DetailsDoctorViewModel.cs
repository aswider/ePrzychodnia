using System.Collections.Generic;
using ePrzychodnia.Core.ViewModels.Visit;

namespace ePrzychodnia.Core.ViewModels.Doctor
{
    public class DetailsDoctorViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Specialization { get; set; }

        public List<VisitViewModel> Visits { get; set; }
    }
}