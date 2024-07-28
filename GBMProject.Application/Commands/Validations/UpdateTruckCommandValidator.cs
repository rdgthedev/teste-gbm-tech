using FluentValidation;
using GBMProject.Application.Commands.Truck;

namespace GBMProject.Application.Commands.Validations;

public class UpdateTruckCommandValidator : AbstractValidator<UpdateTruckCommand>
{
    public UpdateTruckCommandValidator()
    {
        RuleFor(utcv => utcv.Plate)
            .Length(7).WithMessage("A placa deve ter sete digitos");
    }
}