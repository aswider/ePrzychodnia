using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ePrzychodnia.Core.ViewModels.Doctor
{
    public class CreateDoctorViewModel
    {
        [Required(ErrorMessage = "Pole imię jest wymagane")]
        [Display(Name = "Imię")]
        [MaxLength(25, ErrorMessage = "Maksymalna długość imienia to {0} znaków")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Pole nazwisko jest wymagane")]
        [Display(Name = "Nazwisko")]
        [MaxLength(50, ErrorMessage = "Maksymalna długość nazwiska to {0} znaków")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Pole e-mail jest wymagane")]
        [Display(Name = "E-mail")]
        [EmailAddress(ErrorMessage = "Nieprawidłowy adres e-mail")]
        [MaxLength(50, ErrorMessage = "Maksymalna długość e-mail to {0} znaków")]
        public string Email { get; set; }

        [Phone(ErrorMessage = "Nieprawidłowy numer telefonu")]
        [Display(Name = "Nr telefonu")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Specjalizacja jest wymagana")]
        [Display(Name = "Specjalizacja")]
        public string Specialization { get; set; }

        public List<SelectListItem> Specializations { get; set; }
    }
}