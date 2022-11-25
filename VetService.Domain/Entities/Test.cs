using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetService.Domain.ApiModels;
using VetService.Domain.Converters;

namespace VetService.Domain.Entities
{
    public class Test : IConvertModel<Test, TestApiModel>
    {
        public int TestId { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public int AppointmentId { get; set; }

        public TestApiModel Convert() =>
            new TestApiModel
            {
                TestId = this.TestId,
                Date = this.Date,
                Description = this.Description,
                AppointmentId = this.AppointmentId
            };
    }
}

