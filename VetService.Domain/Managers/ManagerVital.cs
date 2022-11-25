using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetService.Domain.ApiModels;
using VetService.Domain.Entities;
using VetService.Domain.Extensions;
using VetService.Domain.Repositories;

namespace VetService.Domain.Managers
{
    public partial class ManagerVital:IManagerVital
    {
        private readonly IVitalRepository _vitalRepository;
        private readonly IMemoryCache _cache;

        public ManagerVital()
        {

        }
        public ManagerVital(IVitalRepository vitalRepository, IMemoryCache cache)
        {

            _vitalRepository = vitalRepository;
            _cache = cache;
        }

        public VitalApiModel AddVital(VitalApiModel newVital)
        {
            var vital = newVital.Convert();

            vital = _vitalRepository.Add(vital);
            newVital.VitalId = vital.VitalId;
            return newVital;
        }

        public async Task<VitalApiModel> AddVitalAsync(VitalApiModel newVital)
        {
            var vital = newVital.Convert();

            vital = await _vitalRepository.AddAsync(vital);
            newVital.VitalId = vital.VitalId;
            return newVital;
        }

        public bool DeleteVital(int id)=> _vitalRepository.Delete(id);

        public async Task<bool> DeleteVitalAsync(int id)
        {
            return await _vitalRepository.DeleteAsync(id);
        }

        public IEnumerable<VitalApiModel> GetAllVital()
        {
            var vitals = _vitalRepository.GetAll().ConvertAll();
            foreach (var vital in vitals)
            {
                var cacheEntryOptions =
                    new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(604800));
                _cache.Set(string.Concat("Vital-", vital.VitalId), vital, cacheEntryOptions);
            }

            return vitals;
        }

        public async Task<IEnumerable<VitalApiModel>> GetAllVitalAsync()
        {
            var vitals = (await _vitalRepository.GetAllAsync()).ConvertAll();
            foreach (var vital in vitals)
            {
                var cacheEntryOptions =
                    new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(604800));
                _cache.Set(string.Concat("Vital-", vital.VitalId), vital, cacheEntryOptions);
            }

            return vitals;
        }

        public async Task<VitalApiModel> GetVitalByIdAsync(int id)
        {
            var vitalApiModelCached = _cache.Get<VitalApiModel>(string.Concat("Vital-", id));

            if (vitalApiModelCached != null)
            {
                return vitalApiModelCached;
            }
            else
            {
                var vital = await _vitalRepository.GetByIdAsync(id);
                if (vital == null) return null;
                var vitalApiModel = vital.Convert();

                var cacheEntryOptions =
                    new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(604800));
                _cache.Set(string.Concat("Vital-", vitalApiModel.VitalId), vitalApiModel, cacheEntryOptions);
                return vitalApiModel;
            }
        }

        public VitalApiModel GetVitaltById(int id)
        {
            var vitalApiModelCached = _cache.Get<VitalApiModel>(string.Concat("Vital-", id));

            if (vitalApiModelCached != null)
            {
                return vitalApiModelCached;
            }
            else
            {
                var vital =  _vitalRepository.GetById(id);
                if (vital == null) return null;
                var vitalApiModel = vital.Convert();

                var cacheEntryOptions =
                    new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(604800));
                _cache.Set(string.Concat("Vital-", vitalApiModel.VitalId), vitalApiModel, cacheEntryOptions);
                return vitalApiModel;
            }
        }

        public bool UpdateVital(VitalApiModel vital)
        {
            var vitals = _vitalRepository.GetById(vital.VitalId);
            if (vitals != null) return false;
            vitals.VitalId = vital.VitalId;
            vitals.AppointmentId = vital.AppointmentId;
            vitals.Temperature = vital.Temperature;
            vitals.HeartRate = vital.HeartRate;
            vitals.BP = vital.BP;

            return _vitalRepository.Update(vitals);
        }

        public async Task<bool> UpdateVitalAsync(VitalApiModel vital)
        {
            var vitals = await _vitalRepository.GetByIdAsync(vital.VitalId);

            if (vitals != null) return false;
            vitals.VitalId = vital.VitalId;
            vitals.AppointmentId = vital.AppointmentId;
            vitals.Temperature = vital.Temperature;
            vitals.HeartRate = vital.HeartRate;
            vitals.BP = vital.BP;

            return await _vitalRepository.UpdateAsync(vitals);
        }
    }
}
