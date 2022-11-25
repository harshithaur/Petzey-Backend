using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetService.Domain.ApiModels;

namespace VetService.Domain.Managers
{
    public interface IManagerVital
    {
        #region Sync Methods
        IEnumerable<VitalApiModel> GetAllVital();
        VitalApiModel GetVitaltById(int id);
        VitalApiModel AddVital(VitalApiModel newVital);
        bool UpdateVital(VitalApiModel vital);
        bool DeleteVital(int id);
        #endregion

        #region Async Methods
        Task<IEnumerable<VitalApiModel>> GetAllVitalAsync();
        Task<VitalApiModel> GetVitalByIdAsync(int id);
        Task<VitalApiModel> AddVitalAsync(VitalApiModel newVital);
        Task<bool> UpdateVitalAsync(VitalApiModel vital);
        Task<bool> DeleteVitalAsync(int id);
        #endregion
    }
}
