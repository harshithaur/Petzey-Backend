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
    public class PrescriptionRepository : IPrescriptionRepository
    {
        private readonly VetServiceDbContext _context;
        public PrescriptionRepository(VetServiceDbContext context)
        {
            _context = context;
        }

        private bool PrescriptionExists(int id) => _context.Prescriptions.Any(x => x.PrescriptionId == id);
        public void Dispose() => _context.Dispose();



        public Prescription Add(Prescription prescription)
        {
            _context.Prescriptions.Add(prescription);
            _context.SaveChanges();
            return prescription;
        }

        public async Task<Prescription> AddAsync(Prescription prescription)
        {
            _context.Prescriptions.Add(prescription);
            await _context.SaveChangesAsync();
            return prescription;
        }

        public bool Delete(int id)
        {
            if (!PrescriptionExists(id))
                return false;
            var toRemove = _context.Prescriptions.Find(id);
            _context.Prescriptions.Remove(toRemove);
            _context.SaveChanges();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            if (!PrescriptionExists(id))
                return false;
            var toRemove = await _context.Prescriptions.FindAsync(id);
            _context.Prescriptions.Remove(toRemove);
            await _context.SaveChangesAsync();
            return true;
        }

        public List<Prescription> GetAll() => _context.Prescriptions.AsNoTracking().ToList();

        public async Task<List<Prescription>> GetAllAsync()
        {
            return await _context.Prescriptions.AsNoTracking().ToListAsync();
        }

        public Prescription GetById(int id)
        {
            var prescription = _context.Prescriptions.Find(id);
            return prescription;
        }

        public async Task<Prescription> GetByIdAsync(int id)
        {
            return await _context.Prescriptions.FindAsync(id);
        }

        public bool Update(Prescription prescription)
        {
            if (!PrescriptionExists(prescription.PrescriptionId))
                return false;
            _context.Prescriptions.Update(prescription);
            _context.SaveChanges();
            return true;
        }

        public async Task<bool> UpdateAsync(Prescription prescription)
        {
            if (!PrescriptionExists(prescription.PrescriptionId))
                return false;
            _context.Prescriptions.Update(prescription);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
