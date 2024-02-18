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

namespace CarWorkshop.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddApplication(this IServiceCollection services)
        {
            //przekazuje interfejs wcześniej wyekstrachowany, a pod jego typem
            //który będzie dostępny w ramach kontenera zależności będzie CarWorkshopService
            services.AddMediatR(typeof(CreateCarWorkshopCommand));
            services.AddAutoMapper(typeof(CarWorkshopMappingProfile));

            services.AddValidatorsFromAssemblyContaining<CreateCarWorkshopCommandValidator>()
                .AddFluentValidationAutoValidation()
                .AddFluentValidationClientsideAdapters();
        }
    }
}
