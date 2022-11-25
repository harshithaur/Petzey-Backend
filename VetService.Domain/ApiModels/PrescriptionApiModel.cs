using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetService.Domain.Converters;
using VetService.Domain.Entities;

namespace VetService.Domain.ApiModels
{
    public class PrescriptionApiModel : IConvertModel<PrescriptionApiModel, Prescription>
    {
        public int PrescriptionId { get; set; }
        public int AppointmentId { get; set; }
        public int MedicineId { get; set; }

        public Prescription Convert() =>
            new Prescription
            {
                PrescriptionId = PrescriptionId,
                AppointmentId = AppointmentId,
                MedicineId = MedicineId
            };
    }
}

