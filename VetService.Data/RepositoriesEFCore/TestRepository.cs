using VetService.Domain.Repositories;
using VetService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetService.Data;
using System.Numerics;
using Microsoft.EntityFrameworkCore;



namespace VetService.Domain.RepositoriesEFCore
{
    public class TestRepository : ITestRepository
    {
        private readonly VetServiceDbContext _context;
        public TestRepository(VetServiceDbContext context)
        {
            _context = context;
        }



        private bool TestExists(int id) => _context.Tests.Any(x => x.TestId == id);
        public void Dispose() => _context.Dispose();



        public Test Add(Test test)
        {
            _context.Tests.Add(test);
            _context.SaveChanges();
            return test;
        }



        public async Task<Test> AddAsync(Test test)
        {
            _context.Tests.Add(test);
            await _context.SaveChangesAsync();
            return test;
        }



        public bool Delete(int id)
        {
            if (!TestExists(id))
                return false;
            var toRemove = _context.Tests.Find(id);
            _context.Tests.Remove(toRemove);
            _context.SaveChanges();
            return true;
        }



        public async Task<bool> DeleteAsync(int id)
        {
            if (!TestExists(id))
                return false;
            var toRemove = await _context.Tests.FindAsync(id);
            _context.Tests.Remove(toRemove);
            await _context.SaveChangesAsync();
            return true;
        }



        public List<Test> GetAll() => _context.Tests.AsNoTracking().ToList();



        public async Task<List<Test>> GetAllAsync()
        {
            return await _context.Tests.AsNoTracking().ToListAsync();
        }



        public Test GetById(int id)
        {
            var test = _context.Tests.Find(id);
            return test;
        }



        public async Task<Test> GetByIdAsync(int id)
        {
            return await _context.Tests.FindAsync(id);
        }

        public bool Update(Test test)
        {
            if (!TestExists(test.TestId))
                return false;
            _context.Tests.Update(test);
            _context.SaveChanges();
            return true;
        }

        public async Task<bool> UpdateAsync(Test test)
        {
            if (!TestExists(test.TestId))
                return false;
            _context.Tests.Update(test);
            await _context.SaveChangesAsync();
            return true;
        }


    }
}
