using AutoMapper;
using CarWorkshop.Application.CarWorkshop;
using CarWorkshop.Application.CarWorkshop.Commands.EditCarWorkshop;
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


            //mapowanie w drugą stronę
            CreateMap<Domain.Entities.CarWorkshop, CarWorkshopDto>()
                .ForMember(dto => dto.Street, opt => opt.MapFrom(src => src.ContactDetails.Street))
                .ForMember(dto => dto.City, opt => opt.MapFrom(src => src.ContactDetails.City))
                .ForMember(dto => dto.PostalCode, opt => opt.MapFrom(src => src.ContactDetails.PostalCode))
                .ForMember(dto => dto.PhoneNumber, opt => opt.MapFrom(src => src.ContactDetails.PhoneNumber));


            CreateMap<CarWorkshopDto, EditCarWorkshopCommand>();
        }
    }
}
