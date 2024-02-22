using AutoMapper;
using CarWorkshop.Application.ApplicationUser;
using CarWorkshop.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarWorkshop.Application.CarWorkshop.Commands.CreateCarWorkshop
{
    //klasa odpowiedzialna za tworzenie warsztatów -> obsługa komendy Create (zmiana implementacji z Servisu)
    public class CreateCarWorkshopCommandHandler : IRequestHandler<CreateCarWorkshopCommand>
    {
        private readonly ICarWorkshopRespository _carWorkshopRepository;
        private readonly IMapper _mapper;
        private readonly IUserContext _userContext;

        public CreateCarWorkshopCommandHandler(ICarWorkshopRespository carWorkshopRepository, IMapper mapper, IUserContext userContext)
        {
            _carWorkshopRepository = carWorkshopRepository;
            _mapper = mapper;
            _userContext = userContext;
        }
        //z obiektu request będziemy w stanie uzyskać wszystkie właściwości od użytkownika
        public async Task<Unit> Handle(CreateCarWorkshopCommand request, CancellationToken cancellationToken)
        {
            var currentUser = _userContext.GetCurrentUser();
            if(currentUser == null || !currentUser.IsInRole("Admin"))
            {
                return Unit.Value;
            }
            //mapuje Dto na konkretny CarWorkshop wywołując metodę Map<na co chce zmapować>(co mapuje)
            var carWorkshop = _mapper.Map<Domain.Entities.CarWorkshop>(request);
            carWorkshop.EncodeName();

            carWorkshop.CreatedById = currentUser.Id;

            //czyli tutaj będę tak właściwie wykonywał Create konkretny warsztat na bazie
            await _carWorkshopRepository.Create(carWorkshop);

            return Unit.Value;
        }
    }
}
