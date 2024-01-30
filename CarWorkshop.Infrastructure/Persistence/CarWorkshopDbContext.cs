using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using CarWorkshop.Domain.Entities;

namespace CarWorkshop.Infrastructure.Persistence
{
    //reorezentacja bazy danych
    public class CarWorkshopDbContext : DbContext
    {
        public DbSet<Domain.Entities.CarWorkshop> CarWorkshops{ get; set; }

        //konfiguracja kontekstu bazy danych, wstrzykuje zależność przez konstruktor
        public CarWorkshopDbContext(DbContextOptions<CarWorkshopDbContext> options) : base(options)
        {
            
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    //przy instalacji VS tworzony jest taki serwer, wiec na windowsie coś takiego powinno działać 
        //    optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=CarWorkshopDb;Trusted_Connection=True");
        //    //trusted-connection -> logowanie windowsowe
        //}

        //metoda, która pomoże w utawieniu modelu CarWorkshop. Model ten ma klasę Details jako właściwość. EF mógłby zrobić z niej osobną tabelę gdybym
        //dał int Id. Ale nie chce tego robić, tylko zaznaczyć że to jest po prostu właściwość!
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Domain.Entities.CarWorkshop>()
                .OwnsOne(c => c.ContactDetails);
        }
    }
}
