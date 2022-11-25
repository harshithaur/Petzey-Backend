using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetService.Domain.Entities;

namespace VetService.Domain.Repositories
{
    public interface IVitalRepository
    {
        #region Sync Methods
        List<Vital> GetAll();
        Vital GetById(int id);
        Vital Add(Vital vital);
        bool Update(Vital vital);
        bool Delete(int id);

        #endregion

        #region Async Methods
        Task<List<Vital>> GetAllAsync();
        Task<Vital> GetByIdAsync(int id);
        Task<Vital> AddAsync(Vital vital);
        Task<bool> UpdateAsync(Vital vital);
        Task<bool> DeleteAsync(int id);
        #endregion
    }
}
