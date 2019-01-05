using System.Collections.Generic;

namespace ePrzychodnia.Data
{
    public class Patient
    {
        public int Id { get; set; }
        public string Pesel { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string PostalCode { get; set; }
        public List<Visit> Visits { get; set; }
    }
}