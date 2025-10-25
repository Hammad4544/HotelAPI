using AutoMapper;
using Models.DTOS.Booking;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Example mapping configurations
            // CreateMap<SourceType, DestinationType>();
            CreateMap<CreateBookingDTO, Booking>();
        }
    }
}
