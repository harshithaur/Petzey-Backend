using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetService.Domain.Repositories;

namespace VetAPITest.Repository
{
    public class PrescriptionRepositoryTest
    {
        private readonly IPrescriptionRepository _repo;

        public PrescriptionRepositoryTest()
        {

        }

        [Test]
        public void GetPrescriptionAll()
        {
            var prescriptions = _repo.GetAll();
            Assert.True(prescriptions.Count > 1, "The number of prescription was not greater then 1 ");
        }
        [Test]
        public void GetOnePrescription()
        {
            // Arrange
            var id = 1;

            // Act
            var prescription = _repo.GetById(id);

            // Assert
            Assert.Equals(id, prescription.PrescriptionId);
        }
    }
}
