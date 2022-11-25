using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetService.Domain.ApiModels;

namespace VetService.Domain.Managers
{
    public interface IManagerDoctor
    {
        #region Sync Methods
        IEnumerable<DoctorApiModel> GetAllDoctor();
        DoctorApiModel GetDoctorById(int id);

        DoctorApiModel AddDoctor(DoctorApiModel newDoctor);

        bool UpdateDoctor(DoctorApiModel doctor);
        bool DeleteDoctor(int id);
        #endregion

        #region Async Methods
        Task<IEnumerable<DoctorApiModel>> GetAllDoctorAsync();
        Task<DoctorApiModel> GetDoctorByIdAsync(int id);

        Task<DoctorApiModel> AddDoctorAsync(DoctorApiModel newDoctor);

        Task<bool> UpdateDoctorAsync(DoctorApiModel doctor);
        Task<bool> DeleteDoctorAsync(int id);
        #endregion
    }
}
