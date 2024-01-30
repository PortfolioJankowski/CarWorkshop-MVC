using CarWorkshop.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarWorkshop.Application.Services
{
    public class CarWorkshopService : ICarWorkshopService
    {
        private readonly ICarWorkshopRespository _carWorkshopRespository;

        //klasa z infrastruktury implementuje ten interfejs z domeny
        public CarWorkshopService(ICarWorkshopRespository carWorkshopRespository)
        {
            _carWorkshopRespository = carWorkshopRespository;
        }

        //tworzenie warsztatu -> na _carWorkshopRepository z infrastruktury
        public async Task Create(Domain.Entities.CarWorkshop carWorkshop)
        {
            carWorkshop.EncodeName();
            //czyli tutaj będę tak właściwie wykonywał Create konkretny warsztat na bazie
            await _carWorkshopRespository.Create(carWorkshop);
        }
    }
}
