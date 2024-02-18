using AutoMapper;
using CarWorkshop.Application.CarWorkshop.Queries.GetAllCarWorkshopQuery;
using CarWorkshop.Application.CarWorkshop.Queries.GetCarWorkshopByEncodedNameQuery;
using CarWorkshop.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarWorkshop.Application.CarWorkshop.Queries.GetCarWorkshopByEncodedName
{
    public class GetCarWorkshopByEncodedNameHandler : IRequestHandler<GetCarWorkshopByEncodedNameQuery.GetCarWorkshopByEncodedNameQuery, CarWorkshopDto>
    {
        private readonly ICarWorkshopRespository _carWorkshopRespository;
        private readonly IMapper _mapper;

        public GetCarWorkshopByEncodedNameHandler(ICarWorkshopRespository carWorkshopRespository, IMapper mapper)
        {
            _carWorkshopRespository = carWorkshopRespository;
            _mapper = mapper;
        }
        public async Task<CarWorkshopDto> Handle(GetCarWorkshopByEncodedNameQuery.GetCarWorkshopByEncodedNameQuery request, CancellationToken cancellationToken)
        {
            var carWorkshop = await _carWorkshopRespository.GetByEncodedName(request.EncodedName);
            var dto = _mapper.Map<CarWorkshopDto>(carWorkshop);
            return dto;
        }
    }
}
