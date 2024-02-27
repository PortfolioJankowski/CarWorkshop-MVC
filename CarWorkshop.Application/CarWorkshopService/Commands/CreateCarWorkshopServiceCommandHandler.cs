using CarWorkshop.Application.ApplicationUser;
using CarWorkshop.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarWorkshop.Application.CarWorkshopService.Commands
{
    public class CreateCarWorkshopServiceCommandHandler : IRequestHandler<CreateCarWorkshopServiceCommand>
    {
        private readonly ICarWorkshopRespository repository;
        private readonly IUserContext context;
        private readonly ICarWorkshopServiceRepository serviceRepository;

        public CreateCarWorkshopServiceCommandHandler(ICarWorkshopRespository repository, IUserContext context, ICarWorkshopServiceRepository serviceRepository)
        {
            this.repository = repository;
            this.context = context;
            this.serviceRepository = serviceRepository;
        }
        //mechanizm skopiowany z EditWorkshopCommandHandlera
        public async Task<Unit> Handle(CreateCarWorkshopServiceCommand request, CancellationToken cancellationToken)
        {
            var carWorkshop = await repository.GetByEncodedName(request.CarWorkshopEndcodedName!);
            var user = context.GetCurrentUser();
            var isEditable = user != null && carWorkshop.CreatedById == user.Id;
            if (!isEditable)
            {
                return Unit.Value;
            }
            var carWorkshopService = new Domain.Entities.CarWorkshopService()
            {
                CarWorkshop = carWorkshop,
                Cost = request.Cost,
                Description = request.Description,
            };
            await serviceRepository.Create(carWorkshopService);
            return Unit.Value;
        }
    }
}
