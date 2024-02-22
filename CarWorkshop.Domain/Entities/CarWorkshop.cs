using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarWorkshop.Domain.Entities
{
    public class CarWorkshop
    {
        //wymagam klucza
        public int Id { get; set; }
        //non nullable
        public string Name { get; set; } = default!;
        //możliwość nulla
        public string? Description { get; set; }
        //utworzenie w czasie aktualnym
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public CarWorkshopContactDetails ContactDetails { get; set; } = default!;

        //Dlaczego to jest nullable?? Bo zmieniam to w trakcie działania tej bazy i istnienia warsztatów
        //do tej pory są warsztaty bez tego więc musze dać nullable bo byłby błąd przy migracji
        public string? CreatedById { get; set; }
        //to jest powiązanie tego pola do tabeli z użytkownikami, musi być 
        //wspólny człon CreatedBy
        public IdentityUser? CreatedBy { get; set; }

        public string? About { get; set; }
        //zmienna, która będzie ustawiać nazwę pod kątem SEO
        public string EncodedName { get; private set; } = default!;

        //metoda, która będzie to ogrywać
        public void EncodeName() => EncodedName = Name.ToLower().Replace(" ", "-");
    }
}
