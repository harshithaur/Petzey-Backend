using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetService.Domain.Entities;

namespace VetService.Domain.Repositories
{
    public interface IPrescriptionRepository
    {
        #region Sync Methods
        List<Prescription> GetAll();
        Prescription GetById(int id);
        Prescription Add(Prescription prescription);
        bool Update(Prescription prescription);
        bool Delete(int id);

        #endregion

        #region Async Methods
        Task<List<Prescription>> GetAllAsync();
        Task<Prescription> GetByIdAsync(int id);
        Task<Prescription> AddAsync(Prescription prescription);
        Task<bool> UpdateAsync(Prescription prescription);
        Task<bool> DeleteAsync(int id);
        #endregion
    }
}
