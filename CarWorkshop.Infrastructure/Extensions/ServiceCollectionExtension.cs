using CarWorkshop.Domain.Interfaces;
using CarWorkshop.Infrastructure.Persistence;
using CarWorkshop.Infrastructure.Repositories;
using CarWorkshop.Infrastructure.Seeders;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarWorkshop.Infrastructure.Extensions
{
    public static class ServiceCollectionExtension
    {
        //IServiceCollection -> bo AddDbContext wewnątrz project.cs = builder.Services.AddDbContext<> ta metoda wywołuje się na IServiceCollection
        //IConfiguration -> to samo co u góry
        //jest to metoda, która będzie ROZSZERZAĆ TYP DANYCH ISERVICECOLLECTION -> statyczna klasa, statyczna metoda, słowo kluczowe this w konstruktorze
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CarWorkshopDbContext>(options => options.UseSqlServer(
            configuration.GetConnectionString("CarWorkshop")));


            //klasa użytkownika (domyślna) - definiuje w sobie kilka właściwości
            //AddEFStores - integracja całego systemu
            services.AddDefaultIdentity<IdentityUser>()
                    .AddRoles<IdentityRole>()
                    .AddEntityFrameworkStores<CarWorkshopDbContext>();

            services.AddScoped<CarWorkshopSeeder>();
            services.AddScoped<ICarWorkshopRespository, CarWorkshopRepository>();
        }
    }
}
