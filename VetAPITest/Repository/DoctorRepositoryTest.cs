using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetService.Domain.Repositories;

namespace VetAPITest.Repository
{
    public class DoctorRepositoryTest
    {
        private readonly IDoctorRepository _repo;

        public DoctorRepositoryTest()
        {

        }

        [Test]
        public void GetAllDoctor()
        {
            var doctor = _repo.GetAll();
            Assert.True(doctor.Count > 1, "The number of doctor was not greater then 1 ");
        }
        [Test]
        public void GetOneDoctor()
        {
            // Arrange
            var id = 1;

            // Act
            var doctor = _repo.GetById(id);

            // Assert
            Assert.Equals(id, doctor.DoctorId);
        }
    }
}
