
using VetService.Data;
using VetService.Domain.Entities;
using VetService.Domain.Repositories;
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
    public class SymptomRepository : ISymptomRepository
    {
        private readonly VetServiceDbContext _context; 
        public SymptomRepository(VetServiceDbContext context) { _context = context; }

        private bool SymptomExists(int id) => _context.Symptoms.Any(x => x.SymptomId == id);
        public void Dispose() => _context.Dispose();

        public Symptom Add(Symptom symptom) 
        { 
            _context.Symptoms.Add(symptom); 
            _context.SaveChanges(); return symptom; 
        }

        public async Task<Symptom> AddAsync(Symptom symptom)
        {
            _context.Symptoms.Add(symptom); await _context.SaveChangesAsync(); 
            return symptom;
        }

        public bool Delete(int id) 
        {
            if (!SymptomExists(id)) return false;
            var toRemove = _context.Symptoms.Find(id);
            _context.Symptoms.Remove(toRemove);
            _context.SaveChanges(); return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            if (!SymptomExists(id)) return false;
            var toRemove = await _context.Symptoms.FindAsync(id);
            _context.Symptoms.Remove(toRemove); 
            await _context.SaveChangesAsync();
            return true;
        }

        public List<Symptom> GetAll() => _context.Symptoms.AsNoTracking().ToList();

        public async Task<List<Symptom>> GetAllAsync()
        {
            return await _context.Symptoms.AsNoTracking().ToListAsync();
        }

        public Symptom GetById(int id)
        {
            var symptom = _context.Symptoms.Find(id);
            return symptom;
        }

        public async Task<Symptom> GetByIdAsync(int id)
        {
            return await _context.Symptoms.FindAsync(id);
        }

        public bool Update(Symptom symptom) 
        { if (!SymptomExists(symptom.SymptomId)) return false;
            _context.Symptoms.Update(symptom);
            _context.SaveChanges(); return true;
        }

        public async Task<bool> UpdateAsync(Symptom symptom) 
        {
            if (!SymptomExists(symptom.SymptomId)) return false;
            _context.Symptoms.Update(symptom);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}

