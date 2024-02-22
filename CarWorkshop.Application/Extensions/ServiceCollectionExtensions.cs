using CarWorkshop.Application.Mappings;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.AspNetCore;
using FluentValidation;
using CarWorkshop.Application.CarWorkshop.Commands.CreateCarWorkshop;
using MediatR;
using CarWorkshop.Application.ApplicationUser;
using AutoMapper;

namespace CarWorkshop.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IUserContext, UserContext>();
            //przekazuje interfejs wcześniej wyekstrachowany, a pod jego typem
            //który będzie dostępny w ramach kontenera zależności będzie CarWorkshopService
            services.AddMediatR(typeof(CreateCarWorkshopCommand));

            //services.AddAutoMapper(typeof(CarWorkshopMappingProfile));

            //rozpoczynam rejestrowanie - dla każdego zakresu życia powstanie nowa instancja tej usługi
            services.AddScoped(provider => new MapperConfiguration(cfg =>
            {
                var scope = provider.CreateScope();
                var userContext = scope.ServiceProvider.GetRequiredService<IUserContext>();
                cfg.AddProfile(new CarWorkshopMappingProfile(userContext));
            }).CreateMapper()
            );

            services.AddValidatorsFromAssemblyContaining<CreateCarWorkshopCommandValidator>()
                .AddFluentValidationAutoValidation()
                .AddFluentValidationClientsideAdapters();
        }
    }
}
