using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetService.Domain.Converters;
using VetService.Domain.Entities;

namespace VetService.Domain.ApiModels
{
    public class VitalApiModel : IConvertModel<VitalApiModel, Vital>
    {
        public int VitalId { get; set; }
        public int HeartRate { get; set; }
        public int Temperature { get; set; }
        public int BP { get; set; }
        public int AppointmentId { get; set; }

        public Vital Convert() =>
           new Vital
           {
               VitalId = VitalId,
               HeartRate = HeartRate,
               Temperature = Temperature,
               BP = BP,
               AppointmentId = AppointmentId
           };
    }
}
