using System.Threading.Tasks;
using ePrzychodnia.Core.ViewModels.Doctor;
using ePrzychodnia.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ePrzychodnia.Web.Controllers
{
    [Authorize]
    public class DoctorController:Controller
    {
        private readonly IDoctorService _doctorService;

        public DoctorController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        [HttpGet]
        public IActionResult Create()
        {
            var model = _doctorService.GetCreateDoctorViewModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateDoctorViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Specializations = _doctorService.GetSpecializations();

                return View(model);
            }

            var doctorId = await _doctorService.CreateDoctor(model);

            return RedirectToAction("Details", new {id = doctorId});
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var model = await _doctorService.GetEditDoctorViewModel(id);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditDoctorViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Specializations = _doctorService.GetSpecializations();

                return View(model);
            }

          await _doctorService.EditDoctor(model);

            return RedirectToAction("Details", new { id = model.Id });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _doctorService.DeleteDoctor(id);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = await _doctorService.GetDoctors();

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var model = await _doctorService.GetDoctorDetails(id);

            return View(model);
        }
    }
}