using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetService.Domain.ApiModels;

namespace VetService.Domain.Managers
{
    public interface IManagerSymptom
    {
        #region Sync Methods
        IEnumerable<SymptomApiModel> GetAllSymptom();
        SymptomApiModel GetSymptomById(int id);

        SymptomApiModel AddSymptom(SymptomApiModel newSymptom);

        bool UpdateSymptom(SymptomApiModel symptom);
        bool DeleteSymptom(int id);


        #endregion

        #region Async Methods
        Task<IEnumerable<SymptomApiModel>> GetAllSymptomAsync();
        Task<SymptomApiModel> GetSymptomByIdAsync(int id);

        Task<SymptomApiModel> AddSymptomAsync(SymptomApiModel newSymptom);

        Task<bool> UpdateSymptomAsync(SymptomApiModel symptom);
        Task<bool> DeleteSymptomAsync(int id);
        #endregion
    }
}
