using CarWorkshop.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarWorkshop.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddApplication(this IServiceCollection services)
        {
            //przekazuje interfejs wcześniej wyekstrachowany, a pod jego typem
            //który będzie dostępny w ramach kontenera zależności będzie CarWorkshopService
            services.AddScoped<ICarWorkshopService, CarWorkshopService>();
        }
    }
}
