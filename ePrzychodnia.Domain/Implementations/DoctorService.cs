using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ePrzychodnia.Core.Enums;
using ePrzychodnia.Core.Exceptions;
using ePrzychodnia.Core.Translations;
using ePrzychodnia.Core.ViewModels.Doctor;
using ePrzychodnia.Core.ViewModels.Visit;
using ePrzychodnia.Data;
using ePrzychodnia.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ErrorCodes = ePrzychodnia.Core.Exceptions.ErrorCodes;

namespace ePrzychodnia.Domain.Implementations
{
    public class DoctorService : IDoctorService
    {
        private readonly ClinicDbContext _dbContext;

        public DoctorService(ClinicDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public CreateDoctorViewModel GetCreateDoctorViewModel()
        {

            return new CreateDoctorViewModel
            {
                Specializations = GetSpecializations()
            };
        }

        public async Task<int> CreateDoctor(CreateDoctorViewModel model)
        {
            var doctor = new Doctor
            {
                Name = model.Name,
                Surname = model.Surname,
                Email = model.Email,
                Phone = model.Phone,
                Specialization = (Specialization)Enum.Parse(typeof(Specialization),model.Specialization),
            };

            try
            {
                await _dbContext.Doctors.AddAsync(doctor);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new ClinicException(ErrorCodes.SavingChangesError, ex);
            }


            return doctor.Id;
        }


        public async Task<EditDoctorViewModel> GetEditDoctorViewModel(int id)
        {
            var doctor = await _dbContext.Doctors.FirstOrDefaultAsync(x => x.Id == id);

            if (doctor == null)
                throw new ClinicException(ErrorCodes.ObjectNotFound);

            return new EditDoctorViewModel
            {
                Id = doctor.Id,
                Name = doctor.Name,
                Surname = doctor.Surname,
                Email = doctor.Email,
                Phone = doctor.Phone,
                Specialization = Specializations.ResourceManager.GetString(doctor.Specialization.ToString()),
                Specializations = GetSpecializations()
            };
        }

        public async Task EditDoctor(EditDoctorViewModel model)
        {
            var doctor = await _dbContext.Doctors.FirstOrDefaultAsync(x => x.Id == model.Id);

            if (doctor == null)
                throw new ClinicException(ErrorCodes.ObjectNotFound);

            doctor.Name = model.Name;
            doctor.Surname = model.Surname;
            doctor.Email = model.Email;
            doctor.Phone = model.Phone;
            doctor.Specialization =(Specialization)Enum.Parse(typeof(Specialization),model.Specialization);

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new ClinicException(ErrorCodes.SavingChangesError, ex);
            }
        }


        public async Task DeleteDoctor(int id)
        {
            var doctor = await _dbContext.Doctors.FirstOrDefaultAsync(x => x.Id == id);

            if (doctor == null)
                throw new ClinicException(ErrorCodes.ObjectNotFound);

            try
            {
                 _dbContext.Doctors.Remove(doctor);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new ClinicException(ErrorCodes.SavingChangesError, ex);
            }
        }

        public async Task<List<DoctorViewModel>> GetDoctors()
        {
            return await _dbContext.Doctors
                .Select(x => new DoctorViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Surname = x.Surname,
                    Specialization =  Specializations.ResourceManager.GetString(x.Specialization.ToString())
                })
                .OrderBy(x=>x.Surname)
                .ThenBy(x=>x.Name)
                .ToListAsync();
        }

        public async Task<DetailsDoctorViewModel> GetDoctorDetails(int id)
        {
            var doctor = await _dbContext.Doctors.FirstOrDefaultAsync(x => x.Id == id);

            if (doctor == null)
                throw new ClinicException(ErrorCodes.ObjectNotFound);

            return new DetailsDoctorViewModel
            {
                Id = doctor.Id,
                Name = doctor.Name,
                Surname = doctor.Surname,
                Email = doctor.Email,
                Phone = doctor.Phone,
                Visits = await GetDoctorVisits(id),
                Specialization = Specializations.ResourceManager.GetString(doctor.Specialization.ToString())
            };
        }



        public List<SelectListItem> GetSpecializations()
        {
            var specializations = new List<SelectListItem>
            {
                new SelectListItem()
            };

            foreach (var specialization in (Specialization[])Enum.GetValues(typeof(Specialization)))
            {
                var value = Specializations.ResourceManager.GetString(specialization.ToString());

                specializations.Add(new SelectListItem()
                {
                    Text = value,
                    Value = specialization.ToString()
                });
            }

            return specializations
                .OrderBy(x => x.Text)
                .ToList();
        }

        private async Task<List<VisitViewModel>> GetDoctorVisits(int doctorId)
        {
            return await _dbContext.Visits
                .Where(x => x.DoctorId == doctorId)
                .Select(x => new VisitViewModel
                {
                    Id = x.Id,
                    Date = x.Date,
                    DoctorId = x.DoctorId,
                    DoctorName = x.Doctor.Name,
                    DoctorSurname = x.Doctor.Surname,
                    PatientId = x.PatientId,
                    PatientName = x.Patient.Name,
                    PatientSurname = x.Patient.Surname,
                    PatientPesel = x.Patient.Pesel
                })
                .OrderBy(x=>x.Date)
                .ToListAsync();
        }
    }
}