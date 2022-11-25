using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using VetService.Domain.ApiModels;
using VetService.Domain.Entities;
using VetService.Domain.Extensions;
using VetService.Domain.Repositories;

namespace VetService.Domain.Managers
{
    public partial class ManagerPrescription:IManagerPrescription
    {
        private readonly IPrescriptionRepository _prescriptionRepository;
        private readonly IMemoryCache _cache;


        public ManagerPrescription(IPrescriptionRepository prescriptionRepository, IMemoryCache cache)
        {

            _prescriptionRepository = prescriptionRepository;
            _cache = cache;
        }

        public PrescriptionApiModel AddPrescription(PrescriptionApiModel newPrescription)
        {
            var prescription = newPrescription.Convert();

            prescription = _prescriptionRepository.Add(prescription);
            newPrescription.PrescriptionId = prescription.PrescriptionId;
            return newPrescription;
        }

        public async Task<PrescriptionApiModel> AddPrescriptionAsync(PrescriptionApiModel newPrescription)
        {
            var prescription = newPrescription.Convert();

            prescription = await _prescriptionRepository.AddAsync(prescription);
            newPrescription.PrescriptionId = prescription.PrescriptionId;
            return newPrescription;
        }

        public bool DeletePrescription(int id)=> _prescriptionRepository.Delete(id);

        public async Task<bool> DeletePrescriptionAsync(int id)
        {
            return await _prescriptionRepository.DeleteAsync(id);
        }

        public IEnumerable<PrescriptionApiModel> GetAllPrescription()
        {
            var prescriptions = _prescriptionRepository.GetAll().ConvertAll();
            foreach (var prescription in prescriptions)
            {
                var cacheEntryOptions =
                    new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(604800));
                _cache.Set(string.Concat("Prescription-", prescription.PrescriptionId), prescription, cacheEntryOptions);
            }

            return prescriptions;
        }

        public async Task<IEnumerable<PrescriptionApiModel>> GetAllPrescriptionAsync()
        {
            var prescriptions = (await _prescriptionRepository.GetAllAsync()).ConvertAll();
            foreach (var prescription in prescriptions)
            {
                var cacheEntryOptions =
                    new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(604800));
                _cache.Set(string.Concat("Prescription-", prescription.PrescriptionId), prescription, cacheEntryOptions);
            }
            return prescriptions;
        }

        public PrescriptionApiModel GetPrescriptionById(int id)
        {
            var prescriptionApiModelCached = _cache.Get<PrescriptionApiModel>(string.Concat("Prescription-", id));

            if (prescriptionApiModelCached != null)
            {
                return prescriptionApiModelCached;
            }
            else
            {
                var prescription = _prescriptionRepository.GetById(id);
                if (prescription == null) return null;
                var prescriptionApiModel = prescription.Convert();

                var cacheEntryOptions =
                    new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(604800));
                _cache.Set(string.Concat("Prescription-", prescriptionApiModel.PrescriptionId), prescriptionApiModel, cacheEntryOptions);

                return prescriptionApiModel;
            }
        }

        public async Task<PrescriptionApiModel> GetPrescriptionByIdAsync(int id)
        {
            var PrescriptionApiModelCached = _cache.Get<PrescriptionApiModel>(string.Concat("Prescription-", id));

            if (PrescriptionApiModelCached != null)
            {
                return PrescriptionApiModelCached;
            }
            else
            {
                var prescription = await _prescriptionRepository.GetByIdAsync(id);
                if (prescription == null) return null;
                var prescriptionApiModel = prescription.Convert();

                var cacheEntryOptions =
                    new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(604800));
                _cache.Set(string.Concat("Prescription-", prescriptionApiModel.PrescriptionId), prescriptionApiModel, cacheEntryOptions);
                return prescriptionApiModel;
            }
        }

        public bool UpdatePrescription(PrescriptionApiModel prescription)
        {
            var prescriptions = _prescriptionRepository.GetById(prescription.PrescriptionId);
            if (prescriptions is null) return false;
            prescriptions.PrescriptionId = prescription.PrescriptionId;
            prescriptions.AppointmentId = prescription.AppointmentId;
            prescriptions.MedicineId = prescription.MedicineId;

            return _prescriptionRepository.Update(prescriptions);
        }

        public async Task<bool> UpdatePrescriptionAsync(PrescriptionApiModel prescription)
        {
            var prescriptions = await _prescriptionRepository.GetByIdAsync(prescription.PrescriptionId);
            if (prescriptions is null) return false;
            prescriptions.PrescriptionId = prescription.PrescriptionId;
            prescriptions.AppointmentId = prescription.AppointmentId;
            prescriptions.MedicineId = prescription.MedicineId;

            return await _prescriptionRepository.UpdateAsync(prescriptions);
        }
    }
}
