using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using VetService.Domain.Entities;
using static System.Net.Mime.MediaTypeNames;

namespace VetService.Domain.Repositories
{
    public interface ITestRepository
    {
        #region Sync Methods
        List<Test> GetAll();
        Test GetById(int id);
        Test Add(Test test);
        bool Update(Test test);
        bool Delete(int id);

        #endregion

        #region Async Methods
        Task<List<Test>> GetAllAsync();
        Task<Test> GetByIdAsync(int id);
        Task<Test> AddAsync(Test test);
        Task<bool> UpdateAsync(Test test);
        Task<bool> DeleteAsync(int id);
        #endregion
    }
}
