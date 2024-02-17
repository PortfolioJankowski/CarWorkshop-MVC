using AutoMapper;
using CarWorkshop.Application.CarWorkshop;
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
        private readonly IMapper _mapper;

        /* klasa z infrastruktury implementuje ten interfejs z domeny
        przez konstruktor wstrzykuję interfejs IMapper - dlaczego?? w metodzie Create chciałbym z CarWorkshopDto robić CarWorkshop do bazy danych
        niestety EF nie ogarnia co to jest to CarWorkshopDto, dlatego musze sobie wstrzyknąć taki interfejs, który umożliwi zmapowanie tego obiektu */
        public CarWorkshopService(ICarWorkshopRespository carWorkshopRespository, IMapper mapper)
        {
            _carWorkshopRespository = carWorkshopRespository;
            _mapper = mapper; 
        }

        //tworzenie warsztatu -> na _carWorkshopRepository z infrastruktury
        public async Task Create(CarWorkshopDto carWorkshopDto)
        {
            //mapuje Dto na konkretny CarWorkshop wywołując metodę Map<na co chce zmapować>(co mapuje)
            var carWorkshop = _mapper.Map<Domain.Entities.CarWorkshop>(carWorkshopDto);
            carWorkshop.EncodeName();
            //czyli tutaj będę tak właściwie wykonywał Create konkretny warsztat na bazie
            await _carWorkshopRespository.Create(carWorkshop);
        }

        //tutaj będę zwracał te wszystkie warsztaty, ale najpierw musze je zmapować jako encje DTO
        public async Task<IEnumerable<CarWorkshopDto>> GetAll()
        {
            var carWorkshops = await _carWorkshopRespository.GetAll();
            var dtos = _mapper.Map<IEnumerable<CarWorkshopDto>>(carWorkshops);
            return dtos;
        }
    }
}
