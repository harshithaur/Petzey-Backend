using VetService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace VetService.Data
{
    public class VetServiceDbContext : DbContext
    {
        public VetServiceDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Test> Tests { get; set; }
        public DbSet<Vital> Vitals { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }
        public DbSet<Symptom> Symptoms { get; set; }
    }
}
