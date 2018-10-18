using AutoMapper;
using EACA_API.Models.Entities;

namespace EACA_API.ViewModels.Mappings
{
    public class ViewModelToEntityMappingProfile : Profile
    {
        public ViewModelToEntityMappingProfile() 
            => CreateMap<RegistrationViewModel, ApiUser>().ForMember(au => au.UserName, map => map.MapFrom(vm => vm.Email));
    }
}
