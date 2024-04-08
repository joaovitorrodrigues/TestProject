using AutoMapper;
using TestProject.Domain.Entities;
using TestProject.Web.Models;

namespace TestProject.Web.AutoMapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Motorcycle, MotorcycleQueryViewModel>();
            CreateMap<Booking, ConfirmBookingViewModel>();
            CreateMap<ConfirmBookingViewModel, ConfirmBookingViewModel>();
        }
    }
}
