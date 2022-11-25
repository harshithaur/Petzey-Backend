using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetService.Domain.Converters;
using VetService.Domain.Entities;

namespace VetService.Domain.ApiModels
{
    public class TestApiModel : IConvertModel<TestApiModel, Test>
    {
        public int TestId { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public int AppointmentId { get; set; }

        public Test Convert() =>
            new Test
            {
                TestId = TestId,
                Date = Date,
                Description = Description,
                AppointmentId = AppointmentId
            };
    }
}

