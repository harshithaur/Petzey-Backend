using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetService.Domain.ApiModels;
using VetService.Domain.Entities;
using VetService.Domain.Extensions;
using VetService.Domain.Repositories;

namespace VetService.Domain.Managers
{
    public partial class ManagerTest:IManagerTest
    {
        private readonly ITestRepository _testRepository;
        private readonly IMemoryCache _cache;

        public ManagerTest()
        {

        }
        public ManagerTest(ITestRepository testRepository, IMemoryCache cache)
        {

            _testRepository = testRepository;
            _cache = cache;
        }

        public TestApiModel AddTest(TestApiModel newTest)
        {
            var test = newTest.Convert();

            test = _testRepository.Add(test);
            newTest.TestId = test.TestId;
            return newTest;
        }

        public async Task<TestApiModel> AddTestAsync(TestApiModel newTest)
        {
            var test = newTest.Convert();

            test = await _testRepository.AddAsync(test);
            newTest.TestId = test.TestId;
            return newTest;
        }

        public bool DeleteTest(int id) => _testRepository.Delete(id);

        public async Task<bool> DeleteTestAsync(int id)
        {
            return await _testRepository.DeleteAsync(id);
        }

        public IEnumerable<TestApiModel> GetAllTest()
        {
            var tests = _testRepository.GetAll().ConvertAll();
            foreach (var test in tests)
            {
                var cacheEntryOptions =
                    new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(604800));
                _cache.Set(string.Concat("Test-", test.TestId), test, cacheEntryOptions);
            }

            return tests;
        }

        public async Task<IEnumerable<TestApiModel>> GetAllTestAsync()
        {
            var tests = (await _testRepository.GetAllAsync()).ConvertAll();
            foreach (var test in tests)
            {
                var cacheEntryOptions =
                    new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(604800));
                _cache.Set(string.Concat("Test-", test.TestId), test, cacheEntryOptions);
            }

            return tests;
        }

        public TestApiModel GetTestById(int id)
        {
            var testApiModelCached = _cache.Get<TestApiModel>(string.Concat("Test-", id));

            if (testApiModelCached != null)
            {
                return testApiModelCached;
            }
            else
            {
                var test = _testRepository.GetById(id);
                if (test == null) return null;
                var testApiModel = test.Convert();

                var cacheEntryOptions =
                    new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(604800));
                _cache.Set(string.Concat("Test-", testApiModel.TestId), testApiModel, cacheEntryOptions);

                return testApiModel;
            }
        }

        public async Task<TestApiModel> GetTestByIdAsync(int id)
        {
            var testApiModelCached = _cache.Get<TestApiModel>(string.Concat("Test-", id));

            if (testApiModelCached != null)
            {
                return testApiModelCached;
            }
            else
            {
                var test = await _testRepository.GetByIdAsync(id);
                if (test == null) return null;
                var testApiModel = test.Convert();

                var cacheEntryOptions =
                    new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(604800));
                _cache.Set(string.Concat("Test-", testApiModel.TestId), testApiModel, cacheEntryOptions);
                return testApiModel;
            }
        }

        public bool UpdateTest(TestApiModel test)
        {
            var tests = _testRepository.GetById(test.TestId);
            if (tests != null) return false;
            tests.TestId=test.TestId;
            tests.Date=test.Date;
            tests.Description=test.Description;
            tests.AppointmentId=test.AppointmentId;

            return _testRepository.Update(tests);
        }

        public async Task<bool> UpdateTestAsync(TestApiModel test)
        {
            var tests = await _testRepository.GetByIdAsync(test.TestId);
            if (tests != null) return false;
            tests.TestId = test.TestId;
            tests.Date = test.Date;
            tests.Description = test.Description;
            tests.AppointmentId = test.AppointmentId;

            return await _testRepository.UpdateAsync(tests);
        }
    }
}
