using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetService.Domain.Converters;
using VetService.Domain.Entities;

namespace VetService.Domain.ApiModels
{
    public class DoctorApiModel :IConvertModel<DoctorApiModel, Doctor>
    {
        public int DoctorId { get; set; }
        public string Name { get; set; }
        public int NPINumber { get; set; }
        public string Speciality { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }

        public int ClinicId { get; set; }

        public Doctor Convert() =>
            new Doctor
            {
                DoctorId = DoctorId,
                Name = Name,
                NPINumber = NPINumber,
                Speciality = Speciality,
                MobileNo = MobileNo,
                Email = Email,
                ClinicId = ClinicId
            };
    }
}
