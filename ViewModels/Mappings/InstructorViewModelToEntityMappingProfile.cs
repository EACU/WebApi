using AutoMapper;
using EACA_API.Models.Account;

namespace EACA_API.ViewModels.Mappings
{
    public class InstructorViewModelToEntityMappingProfile : Profile
    {
        public InstructorViewModelToEntityMappingProfile() 
            => CreateMap<RegistrationInstructorViewModel, ApiUser>()
                .ForMember(au => au.UserName, map => map.MapFrom(vm => vm.Email))
                .ForMember(au => au.PhoneNumber, map => map.MapFrom(vm => vm.PhoneNumber));
    }
}
