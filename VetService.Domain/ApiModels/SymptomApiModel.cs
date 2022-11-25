using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetService.Domain.Converters;
using VetService.Domain.Entities;

namespace VetService.Domain.ApiModels
{
    public class SymptomApiModel : IConvertModel<SymptomApiModel, Symptom>
    {
        public int SymptomId { get; set; }
        public int AppointmentId { get; set; }

        public Symptom Convert() =>
            new Symptom
            {
                SymptomId = SymptomId,
                AppointmentId = AppointmentId
            };
    }
}


