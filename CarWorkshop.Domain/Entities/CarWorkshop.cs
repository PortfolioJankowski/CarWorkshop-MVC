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

        public string? About { get; set; }
        //zmienna, która będzie ustawiać nazwę pod kątem SEO
        public string EncodedName { get; private set; } = default!;

        //metoda, która będzie to ogrywać
        public void EncodeName() => EncodedName = Name.ToLower().Replace(" ", "-");
    }
}
