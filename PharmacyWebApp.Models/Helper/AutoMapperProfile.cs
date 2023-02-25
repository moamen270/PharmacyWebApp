using AutoMapper;
using PharmacyWebApp.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyWebApp.Models.Helper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Prescription, PrescriptionVM>()
                .ForMember(p => p.Patients, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<PrescriptionDetails, PrescriptionDetailsVM>()
                .ReverseMap();
        }
    }
}