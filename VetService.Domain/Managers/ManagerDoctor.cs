using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetService.Domain.Repositories;
using Microsoft.Extensions.Caching.Memory;
using VetService.Domain.ApiModels;
using VetService.Domain.Extensions;

namespace VetService.Domain.Managers
{
    public partial class ManagerDoctor : IManagerDoctor
    {
        private readonly IDoctorRepository _doctorRepository;
        private readonly IMemoryCache _cache;

        public ManagerDoctor()
        {

        }

        public ManagerDoctor(IDoctorRepository doctorRepository, IMemoryCache cache)
        {
            _doctorRepository = doctorRepository;
            _cache = cache;
        }

        public DoctorApiModel AddDoctor(DoctorApiModel newDoctor)
        {
            var doctor = newDoctor.Convert();

            doctor = _doctorRepository.Add(doctor);
            newDoctor.DoctorId = doctor.DoctorId;
            return newDoctor;
        }

        public async Task<DoctorApiModel> AddDoctorAsync(DoctorApiModel newDoctor)
        {
            var doctor = newDoctor.Convert();

            doctor = await _doctorRepository.AddAsync(doctor);
            newDoctor.DoctorId = doctor.DoctorId;
            return newDoctor;
        }

        public bool DeleteDoctor(int id) => _doctorRepository.Delete(id);

        public async Task<bool> DeleteDoctorAsync(int id)
        {
            return await _doctorRepository.DeleteAsync(id);
        }

        public IEnumerable<DoctorApiModel> GetAllDoctor()
        {
            var doctors = _doctorRepository.GetAll().ConvertAll();
            foreach (var doctor in doctors)
            {
                var cacheEntryOptions =
                    new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(604800));
                _cache.Set(string.Concat("Doctor-", doctor.DoctorId), doctor, cacheEntryOptions);
            }

            return doctors;
        }

        public async Task<IEnumerable<DoctorApiModel>> GetAllDoctorAsync()
        {
            var doctors = (await _doctorRepository.GetAllAsync()).ConvertAll();
            foreach (var doctor in doctors)
            {
                var cacheEntryOptions =
                    new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(604800));
                _cache.Set(string.Concat("Doctor-", doctor.DoctorId), doctor, cacheEntryOptions);
            }

            return doctors;

        }

        public DoctorApiModel GetDoctorById(int id)
        {
            var doctorApiModelCached = _cache.Get<DoctorApiModel>(string.Concat("Doctor-", id));

            if (doctorApiModelCached != null)
            {
                return doctorApiModelCached;
            }
            else
            {
                var doctor = _doctorRepository.GetById(id);
                if (doctor == null) return null;
                var doctorApiModel = doctor.Convert();

                var cacheEntryOptions =
                    new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(604800));
                _cache.Set(string.Concat("Doctor-", doctorApiModel.DoctorId), doctorApiModel, cacheEntryOptions);

                return doctorApiModel;
            }
        }

        public async Task<DoctorApiModel> GetDoctorByIdAsync(int id)
        {
            var doctorApiModelCached = _cache.Get<DoctorApiModel>(string.Concat("Doctor-", id));

            if (doctorApiModelCached != null)
            {
                return doctorApiModelCached;
            }
            else
            {
                var doctor = await _doctorRepository.GetByIdAsync(id);
                if (doctor == null) return null;
                var doctorApiModel = doctor.Convert();

                var cacheEntryOptions =
                    new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(604800));
                _cache.Set(string.Concat("Doctor-", doctorApiModel.DoctorId), doctorApiModel, cacheEntryOptions);

                return doctorApiModel;
            }
        }

        public bool UpdateDoctor(DoctorApiModel doctor)
        {
            var doctors = _doctorRepository.GetById(doctor.DoctorId);

            if (doctors is null) return false;
            doctors.DoctorId = doctor.DoctorId;
            doctors.Name = doctor.Name;
            doctors.NPINumber = doctor.NPINumber;
            doctors.Speciality = doctor.Speciality;
            doctors.MobileNo = doctor.MobileNo;
            doctors.Email = doctor.Email;
            doctors.ClinicId = doctor.ClinicId;

            return _doctorRepository.Update(doctors);
        }

        public async Task<bool> UpdateDoctorAsync(DoctorApiModel doctor)
        {
            var doctors = await _doctorRepository.GetByIdAsync(doctor.DoctorId);
            if (doctors is null) return false;
            doctors.DoctorId = doctor.DoctorId;
            doctors.Name = doctor.Name;
            doctors.NPINumber = doctor.NPINumber;
            doctors.Speciality = doctor.Speciality;
            doctors.MobileNo = doctor.MobileNo;
            doctors.Email = doctor.Email;
            doctors.ClinicId = doctor.ClinicId;

            return await _doctorRepository.UpdateAsync(doctors);

        }
    }
}
