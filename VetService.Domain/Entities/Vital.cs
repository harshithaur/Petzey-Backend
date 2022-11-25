using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetService.Domain.ApiModels;
using VetService.Domain.Converters;

namespace VetService.Domain.Entities
{
    public class Vital : IConvertModel<Vital, VitalApiModel>
    {
        public int VitalId { get; set; }
        public int HeartRate { get; set; }
        public int Temperature { get; set; }
        public int BP { get; set; }
        public int AppointmentId { get; set; }

        public VitalApiModel Convert() =>
           new VitalApiModel
           {
               VitalId = VitalId,
               HeartRate = HeartRate,
               Temperature = Temperature,
               BP = BP,
               AppointmentId = AppointmentId
           };
    }
}
