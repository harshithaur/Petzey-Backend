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
    public class VitalRepository : IVitalRepository
    {
        private readonly VetServiceDbContext _context;
        public VitalRepository(VetServiceDbContext context)
        {
            _context = context;
        }



        private bool VitalExists(int id) => _context.Vitals.Any(x => x.VitalId == id);
        public void Dispose() => _context.Dispose();



        public Vital Add(Vital vital)
        {
            _context.Vitals.Add(vital);
            _context.SaveChanges();
            return vital;
        }



        public async Task<Vital> AddAsync(Vital vital)
        {
            _context.Vitals.Add(vital);
            await _context.SaveChangesAsync();
            return vital;
        }



        public bool Delete(int id)
        {
            if (!VitalExists(id))
                return false;
            var toRemove = _context.Vitals.Find(id);
            _context.Vitals.Remove(toRemove);
            _context.SaveChanges();
            return true;
        }



        public async Task<bool> DeleteAsync(int id)
        {
            if (!VitalExists(id))
                return false;
            var toRemove = await _context.Vitals.FindAsync(id);
            _context.Vitals.Remove(toRemove);
            await _context.SaveChangesAsync();
            return true;
        }



        public List<Vital> GetAll() => _context.Vitals.AsNoTracking().ToList();



        public async Task<List<Vital>> GetAllAsync()
        {
            return await _context.Vitals.AsNoTracking().ToListAsync();
        }



        public Vital GetById(int id)
        {
            var vital = _context.Vitals.Find(id);
            return vital;
        }



        public async Task<Vital> GetByIdAsync(int id)
        {
            return await _context.Vitals.FindAsync(id);
        }



        public bool Update(Vital vital)
        {
            if (!VitalExists(vital.VitalId))
                return false;
            _context.Vitals.Update(vital);
            _context.SaveChanges();
            return true;
        }



        public async Task<bool> UpdateAsync(Vital vital)
        {
            if (!VitalExists(vital.VitalId))
                return false;
            _context.Vitals.Update(vital);
            await _context.SaveChangesAsync();
            return true;
        }



    }
}