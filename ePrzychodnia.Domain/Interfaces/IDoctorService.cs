using System.Collections.Generic;
using System.Threading.Tasks;
using ePrzychodnia.Core.ViewModels.Doctor;
using ePrzychodnia.Core.ViewModels.Visit;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ePrzychodnia.Domain.Interfaces
{
    public interface IDoctorService
    {
        CreateDoctorViewModel GetCreateDoctorViewModel();
        Task<int> CreateDoctor(CreateDoctorViewModel model);
        Task<EditDoctorViewModel> GetEditDoctorViewModel(int id);
        Task EditDoctor(EditDoctorViewModel model);
        Task DeleteDoctor(int id);
        Task<List<DoctorViewModel>> GetDoctors();
        Task<DetailsDoctorViewModel> GetDoctorDetails(int id);
        List<SelectListItem> GetSpecializations();
    }
}