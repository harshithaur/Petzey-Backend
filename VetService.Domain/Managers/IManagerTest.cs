using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetService.Domain.ApiModels;

namespace VetService.Domain.Managers
{
    public interface IManagerTest
    {
        #region Sync Methods
        IEnumerable<TestApiModel> GetAllTest();
        TestApiModel GetTestById(int id);
        TestApiModel AddTest(TestApiModel newTest);
        bool UpdateTest(TestApiModel test);
        bool DeleteTest(int id);


        #endregion

        #region Async Methods
        Task<IEnumerable<TestApiModel>> GetAllTestAsync();
        Task<TestApiModel> GetTestByIdAsync(int id);
        Task<TestApiModel> AddTestAsync(TestApiModel newTest);
        Task<bool> UpdateTestAsync(TestApiModel test);
        Task<bool> DeleteTestAsync(int id);
        #endregion
    }
}
