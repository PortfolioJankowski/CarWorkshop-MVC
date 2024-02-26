using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using CarWorkshop.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace CarWorkshop.Infrastructure.Persistence
{
    //reorezentacja bazy danych
    public class CarWorkshopDbContext : IdentityDbContext
    {
        //konfiguracja kontekstu bazy danych, wstrzykuje zależność przez konstruktor
        public CarWorkshopDbContext(DbContextOptions<CarWorkshopDbContext> options) : base(options)
        {
            
        }
        public DbSet<Domain.Entities.CarWorkshop> CarWorkshops { get; set; }
        public DbSet<Domain.Entities.CarWorkshopService> Services { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Domain.Entities.CarWorkshop>()
                .OwnsOne(c => c.ContactDetails);

            //ustawiamy, że Warsztat ma wiele serwisów, ale jeden serwis ma 1 warsztat + klucz obcy
            modelBuilder.Entity<Domain.Entities.CarWorkshop>()
                 .HasMany(c => c.Services)
                 .WithOne(s => s.CarWorkshop)
                 .HasForeignKey(s => s.CarWorkshopId);
      
        }
    }
}
