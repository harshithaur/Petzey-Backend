using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetService.Domain.ApiModels;
using VetService.Domain.Converters;

namespace VetService.Domain.Entities
{
    public class Symptom : IConvertModel<Symptom, SymptomApiModel>
    {
        public int SymptomId { get; set; }
        public int AppointmentId { get; set; }

        public SymptomApiModel Convert() =>
            new SymptomApiModel
            {
                SymptomId = SymptomId,
                AppointmentId = AppointmentId,
            };
    }
}
