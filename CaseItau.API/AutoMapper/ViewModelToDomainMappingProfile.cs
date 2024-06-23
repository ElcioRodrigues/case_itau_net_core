using AutoMapper;
using CaseItau.API.Model;
using CaseItau.Domain;

namespace CaseItau.API.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {

        public ViewModelToDomainMappingProfile() : this("Profile") { }

        public ViewModelToDomainMappingProfile(string profileName) : base(profileName)
        {
            CreateMap<FundoVM, Fundo>().ReverseMap();
            CreateMap<TipoFundoVM, TipoFundo>().ReverseMap();
        }  
    }
}
