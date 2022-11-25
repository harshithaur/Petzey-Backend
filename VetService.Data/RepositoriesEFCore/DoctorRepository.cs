using VetService.Domain.Repositories;
using VetService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetService.Data;
using Microsoft.EntityFrameworkCore;



namespace VetService.Domain.RepositoriesEFCore
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly VetServiceDbContext _context;
        public DoctorRepository(VetServiceDbContext context)
        {
            _context = context;
        }

        private bool DoctorExists(int id) => _context.Doctors.Any(x => x.DoctorId == id);
        public void Dispose() => _context.Dispose();



        public Doctor Add(Doctor doctor)
        {
            _context.Doctors.Add(doctor);
            _context.SaveChanges();
            return doctor;
        }



        public async Task<Doctor> AddAsync(Doctor doctor)
        {
            _context.Doctors.Add(doctor);
            await _context.SaveChangesAsync();
            return doctor;
        }



        public bool Delete(int id)
        {
            if (!DoctorExists(id))
                return false;
            var toRemove = _context.Doctors.Find(id);
            _context.Doctors.Remove(toRemove);
            _context.SaveChanges();
            return true;
        }



        public async Task<bool> DeleteAsync(int id)
        {
            if (!DoctorExists(id))
                return false;
            var toRemove = await _context.Doctors.FindAsync(id);
            _context.Doctors.Remove(toRemove);
            await _context.SaveChangesAsync();
            return true;
        }



        public List<Doctor> GetAll() => _context.Doctors.AsNoTracking().ToList();



        public async Task<List<Doctor>> GetAllAsync()
        {
            return await _context.Doctors.AsNoTracking().ToListAsync();
        }



        public Doctor GetById(int id)
        {
            var doctor = _context.Doctors.Find(id);
            return doctor;
        }



        public async Task<Doctor> GetByIdAsync(int id)
        {
            return await _context.Doctors.FindAsync(id);
        }



        public bool Update(Doctor doctor)
        {
            if (!DoctorExists(doctor.DoctorId))
                return false;
            _context.Doctors.Update(doctor);
            _context.SaveChanges();
            return true;
        }



        public async Task<bool> UpdateAsync(Doctor doctor)
        {
            if (!DoctorExists(doctor.DoctorId))
                return false;
            _context.Doctors.Update(doctor);
            await _context.SaveChangesAsync();
            return true;
        }



    }
}