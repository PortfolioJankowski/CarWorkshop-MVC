using AutoMapper;
using CarWorkshop.Application.ApplicationUser;
using CarWorkshop.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarWorkshop.Application.CarWorkshop.Commands.EditCarWorkshop
{
    public class EditCarWorkshopCommandHandler : IRequestHandler<EditCarWorkshopCommand>
    {
        private readonly ICarWorkshopRespository _carWorkshopRepository;
        private readonly IUserContext _userContext;

        public EditCarWorkshopCommandHandler(ICarWorkshopRespository carWorkshopRespository, IUserContext userContext)
        {
            _carWorkshopRepository = carWorkshopRespository;
            _userContext = userContext;
        }

        public async Task<Unit> Handle(EditCarWorkshopCommand request, CancellationToken cancellationToken)
        {
            var carWorkshop = await _carWorkshopRepository.GetByEncodedName(request.EncodedName!);
            var user = _userContext.GetCurrentUser();
            var isEditable = user != null && carWorkshop.CreatedById == user.Id;
            if(!isEditable)
            {
                return Unit.Value;
            }
            carWorkshop.Description = request.Description;
            carWorkshop.About = request.About;
            carWorkshop.ContactDetails.City = request.City;
            carWorkshop.ContactDetails.PhoneNumber = request.PhoneNumber;
            carWorkshop.ContactDetails.PostalCode = request.PostalCode;
            carWorkshop.ContactDetails.Street = request.Street;
            if (user.IsInRole("Moderator"))
            {
                await _carWorkshopRepository.Commit();
            }
            return Unit.Value;
        }
    }
}
