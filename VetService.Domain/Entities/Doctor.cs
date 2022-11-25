using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetService.Domain.ApiModels;
using VetService.Domain.Converters;

namespace VetService.Domain.Entities
{
    public class Doctor : IConvertModel<Doctor, DoctorApiModel>
    {
        public int DoctorId { get; set; }
        public string Name { get; set; }
        public int NPINumber { get; set; }
        public string Speciality { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }

        public int ClinicId { get; set; }

        public DoctorApiModel Convert() =>
            new DoctorApiModel
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

