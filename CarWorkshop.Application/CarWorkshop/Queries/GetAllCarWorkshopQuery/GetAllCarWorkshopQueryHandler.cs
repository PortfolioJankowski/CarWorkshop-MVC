using AutoMapper;
using CarWorkshop.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarWorkshop.Application.CarWorkshop.Queries.GetAllCarWorkshopQuery
{
    public class GetAllCarWorkshopQueryHandler : IRequestHandler<GetAllCarWorkshopQuery, IEnumerable<CarWorkshopDto>>
    {
        private readonly ICarWorkshopRespository _carWorkshopRespository;
        private readonly IMapper _mapper;

        public GetAllCarWorkshopQueryHandler(ICarWorkshopRespository carWorkshopRespository, IMapper mapper)
        {
            _carWorkshopRespository = carWorkshopRespository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CarWorkshopDto>> Handle(GetAllCarWorkshopQuery request, CancellationToken cancellationToken)
        {
            var carWorkshops = await _carWorkshopRespository.GetAll();
            var dtos = _mapper.Map<IEnumerable<CarWorkshopDto>>(carWorkshops);
            return dtos;
        }
    }
}
