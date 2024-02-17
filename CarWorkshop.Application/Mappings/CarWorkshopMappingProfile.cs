using AutoMapper;
using CarWorkshop.Application.CarWorkshop;
using CarWorkshop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarWorkshop.Application.Mappings
{
    public class CarWorkshopMappingProfile : Profile
    {
        public CarWorkshopMappingProfile()
        {
            /* wywołuję metodę CreateMap, która przyjmuje dwa generyczne argumenty
            pierwszy z nich to obiekt, który chce mapować, a drugi to na jaki obiekt chce mapować */
            CreateMap<CarWorkshopDto, Domain.Entities.CarWorkshop>()
                /* ForMember definiuje mapowanie konkretnej właściwości (ContactDetails), wykorzystując konkretną regułę
                 że tworzymy nowy obiekt CarWorkshopDetails */
                .ForMember(e => e.ContactDetails, opt => opt.MapFrom(src => new CarWorkshopContactDetails()
                {
                    City = src.City,
                    PhoneNumber = src.PhoneNumber,
                    PostalCode = src.PostalCode,
                    Street = src.Street,
                }));
            //czemu nie mapuje reszty? Automaper jest na tyle ogarnięty, że sam zmapuje właściwości o tej samej nazwie i typie
        }
    }
}
