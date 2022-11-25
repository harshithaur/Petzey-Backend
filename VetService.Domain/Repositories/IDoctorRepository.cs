using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using VetService.Domain.Entities;

namespace VetService.Domain.Repositories
{
    public interface IDoctorRepository : IDisposable
    {

        #region Sync Methods
        List<Doctor> GetAll();
        Doctor GetById(int id);
        Doctor Add(Doctor doctor);
        bool Update(Doctor doctor);
        bool Delete(int id);

        #endregion

        #region Async Methods
        Task<List<Doctor>> GetAllAsync();
        Task<Doctor> GetByIdAsync(int id);
        Task<Doctor> AddAsync(Doctor doctor);
        Task<bool> UpdateAsync(Doctor doctor);
        Task<bool> DeleteAsync(int id);
        #endregion



    }
}
