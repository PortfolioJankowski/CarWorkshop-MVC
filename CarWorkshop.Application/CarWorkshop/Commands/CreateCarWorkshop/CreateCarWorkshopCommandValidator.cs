using CarWorkshop.Domain.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarWorkshop.Application.CarWorkshop.Commands.CreateCarWorkshop
{
    public class CreateCarWorkshopCommandValidator : AbstractValidator<CreateCarWorkshopCommand>
    {
        //carWorkshopDtoValidator jest zarejestrowana w DI to będzie miała dostęp do ICarWorkshopRepository
        public CreateCarWorkshopCommandValidator(ICarWorkshopRespository repository)
        {
            RuleFor(c => c.Name)
                .NotEmpty()
                .MinimumLength(2).WithMessage("Name should have at least 2 characters")
                .MaximumLength(20).WithMessage("Name should have maximum of 20 characters")
                .Custom((value, context) =>
                {
                    //ten result jest po to, że GetByName zwraca Taska a ja chce rezultat
                    var existingCarWorkshop = repository.GetByName(value).Result;
                    if (existingCarWorkshop != null)
                    {
                        //jeżeli nie mamy nulla to znaczy że w bazie jest już takie imię
                        context.AddFailure($"{value} is not unique name for car workshop");
                    }
                });

            RuleFor(c => c.Description)
                .NotEmpty().WithMessage("Please enter description");

            RuleFor(c => c.PhoneNumber)
                .MinimumLength(8)
                .MaximumLength(12);
        }
    }
}
