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
    public partial class ManagerSymptom:IManagerSymptom
    {
        private readonly ISymptomRepository _symptomRepository;
        private readonly IMemoryCache _cache;

        public ManagerSymptom()
        {

        }
        public ManagerSymptom(ISymptomRepository symptomRepository, IMemoryCache cache)
        {

            _symptomRepository = symptomRepository;
            _cache = cache;
        }

        public SymptomApiModel AddSymptom(SymptomApiModel newSymptom)
        {
            var symptom = newSymptom.Convert();

            symptom = _symptomRepository.Add(symptom);
            newSymptom.SymptomId = symptom.SymptomId;
            return newSymptom;
        }

        public async Task<SymptomApiModel> AddSymptomAsync(SymptomApiModel newSymptom)
        {
            var symptom = newSymptom.Convert();

            symptom = await _symptomRepository.AddAsync(symptom);
            newSymptom.SymptomId = symptom.SymptomId;
            return newSymptom;
        }

        public bool DeleteSymptom(int id) => _symptomRepository.Delete(id);

        public async Task<bool> DeleteSymptomAsync(int id)
        {
            return await _symptomRepository.DeleteAsync(id);
        }

        public IEnumerable<SymptomApiModel> GetAllSymptom()
        {
            var symptoms = _symptomRepository.GetAll().ConvertAll();
            foreach (var symptom in symptoms)
            {
                var cacheEntryOptions =
                    new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(604800));
                _cache.Set(string.Concat("Symptom-", symptom.SymptomId), symptom, cacheEntryOptions);
            }

            return symptoms;
        }

        public async Task<IEnumerable<SymptomApiModel>> GetAllSymptomAsync()
        {
            var symptoms = (await _symptomRepository.GetAllAsync()).ConvertAll();
            foreach (var symptom in symptoms)
            {
                var cacheEntryOptions =
                    new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(604800));
                _cache.Set(string.Concat("Symptom-", symptom.SymptomId), symptom, cacheEntryOptions);
            }

            return symptoms;
        }

        public SymptomApiModel GetSymptomById(int id)
        {
            var symptomApiModelCached = _cache.Get<SymptomApiModel>(string.Concat("Symptom-", id));

            if (symptomApiModelCached != null)
            {
                return symptomApiModelCached;
            }
            else
            {
                var symptom = _symptomRepository.GetById(id);
                if (symptom == null) return null;
                var symptomApiModel = symptom.Convert();

                var cacheEntryOptions =
                    new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(604800));
                _cache.Set(string.Concat("Symptom-", symptomApiModel.SymptomId), symptomApiModel, cacheEntryOptions);

                return symptomApiModel;
            }
        }

        public async Task<SymptomApiModel> GetSymptomByIdAsync(int id)
        {
            var symptomApiModelCached = _cache.Get<SymptomApiModel>(string.Concat("Symptom-", id));

            if (symptomApiModelCached != null)
            {
                return symptomApiModelCached;
            }
            else
            {
                var symptom = await _symptomRepository.GetByIdAsync(id);
                if (symptom == null) return null;
                var symptomApiModel = symptom.Convert();

                var cacheEntryOptions =
                    new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(604800));
                _cache.Set(string.Concat("Symptom-", symptomApiModel.SymptomId), symptomApiModel, cacheEntryOptions);
                return symptomApiModel;
            }
        }

        public bool UpdateSymptom(SymptomApiModel symptom)
        {
            var symptoms = _symptomRepository.GetById(symptom.SymptomId);
            if (symptoms is null) return false;
            symptoms.SymptomId = symptom.SymptomId;
            symptoms.AppointmentId = symptom.AppointmentId;

            return _symptomRepository.Update(symptoms);
        }

        public async Task<bool> UpdateSymptomAsync(SymptomApiModel symptom)
        {
            var symptoms = await _symptomRepository.GetByIdAsync(symptom.SymptomId);
            if (symptoms is null) return false;
            symptoms.SymptomId = symptom.SymptomId;
            symptoms.AppointmentId = symptom.AppointmentId;

            return await _symptomRepository.UpdateAsync(symptoms);
        }
    }
}
