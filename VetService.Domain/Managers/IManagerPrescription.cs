using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetService.Domain.ApiModels;

namespace VetService.Domain.Managers
{
    public interface IManagerPrescription
    {
        #region Sync Methods
        IEnumerable<PrescriptionApiModel> GetAllPrescription();
        PrescriptionApiModel GetPrescriptionById(int id);

        PrescriptionApiModel AddPrescription(PrescriptionApiModel newPrescription);

        bool UpdatePrescription(PrescriptionApiModel prescription);
        bool DeletePrescription(int id);

      
        #endregion

        #region Async Methods
        Task<IEnumerable<PrescriptionApiModel>> GetAllPrescriptionAsync();
        Task<PrescriptionApiModel> GetPrescriptionByIdAsync(int id);

        Task<PrescriptionApiModel> AddPrescriptionAsync(PrescriptionApiModel newPrescription);

        Task<bool> UpdatePrescriptionAsync(PrescriptionApiModel prescription);
        Task<bool> DeletePrescriptionAsync(int id);
        #endregion
    }
}
