using CaseItau.Infra.Data.Repository;
using CaseItau.Infra.Data.UoW;
using CaseItau.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System.Net;

namespace CaseItau.API.Configuration
{
    public static class ServiceRepositoryCollectionExtensions
    {
        public static IServiceCollection RegisterRepositoryServices(
           this IServiceCollection services)
        {
            //services
            services.AddScoped<IFundoService, FundoService>();
            services.AddScoped<ITipoFundoService, TipoFundoService>();

            //repositories
            services.AddScoped<IFundoRepository, FundoRepository>();
            services.AddScoped<ITipoFundoRepository, TipoFundoRepository>();

            //UnitOfWork
            services.AddScoped<IUnitOfWork, UnitOfWork>();


            return services;
        }
    }
}
