using AutoMapper;
using CaseItau.API.Model;
using CaseItau.Domain;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CaseItau.API.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile() : this("Profile") { }

        public DomainToViewModelMappingProfile(string profileName) : base(profileName)
        {
            CreateMap<Fundo, FundoVM>().ReverseMap();
            CreateMap<TipoFundo, TipoFundoVM>().ReverseMap();
        }
    }
}
