using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetService.Domain.Entities;

namespace VetService.Domain.Repositories
{
    public interface ISymptomRepository
    {
        #region Sync Methods
        List<Symptom> GetAll();
        Symptom GetById(int id);
        Symptom Add(Symptom symptom);
        bool Update(Symptom symptom);
        bool Delete(int id);

        #endregion

        #region Async Methods
        Task<List<Symptom>> GetAllAsync();
        Task<Symptom> GetByIdAsync(int id);
        Task<Symptom> AddAsync(Symptom symptom);
        Task<bool> UpdateAsync(Symptom symptom);
        Task<bool> DeleteAsync(int id);
        #endregion
    }
}
