using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetService.Domain.ApiModels;
using VetService.Domain.Converters;

namespace VetService.Domain.Entities
{
    public class Prescription : IConvertModel<Prescription, PrescriptionApiModel>
    {
        public int PrescriptionId { get; set; }
        public int AppointmentId { get; set; }
        public int MedicineId { get; set; }

        public PrescriptionApiModel Convert() =>
            new PrescriptionApiModel
            {
                PrescriptionId = PrescriptionId,
                AppointmentId = AppointmentId,

                MedicineId = MedicineId
            };
    }
}

