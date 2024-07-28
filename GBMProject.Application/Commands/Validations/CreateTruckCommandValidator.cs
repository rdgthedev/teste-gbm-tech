using FluentValidation;
using GBMProject.Application.Commands.Truck;

namespace GBMProject.Application.Commands.Validations;

public class CreateTruckCommandValidator : AbstractValidator<CreateTruckCommand>
{
    public CreateTruckCommandValidator()
    {
        RuleFor(ctcv => ctcv.Plate)
            .NotEmpty().WithMessage("A placa é obrigatória")
            .Length(7).WithMessage("A placa deve ter sete digitos");
        
        RuleFor(ctcv => ctcv.Model)
            .NotEmpty().WithMessage("O modelo é obrigatório");
        
        RuleFor(ctcv => ctcv.Color)
            .NotEmpty().WithMessage("O modelo é obrigatório");
        
        RuleFor(ctcv => ctcv.NumberOfAxles)
            .GreaterThan(0).WithMessage("O número de eixos deve ser maior que zero");
    }
}