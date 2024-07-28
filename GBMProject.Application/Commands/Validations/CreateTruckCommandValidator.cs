using FluentValidation;
using GBMProject.Application.Commands.Truck;

namespace GBMProject.Application.Commands.Validations;

public class CreateTruckCommandValidator : AbstractValidator<CreateTruckCommand>
{
    private const int _maxYearsOfUse = 15;
    private int _minYearOfManifacture = DateTime.Now.Date.AddYears(-1 * _maxYearsOfUse).Year;
    private int _maxYearOfManifacture = DateTime.Now.Date.Year;

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
            .GreaterThanOrEqualTo(2).WithMessage("O caminhão deve ter no mínimo 2 eixos")
            .LessThanOrEqualTo(20).WithMessage("O caminhão pode ter no máximo 20 eixos");

        RuleFor(ctcv => ctcv.YearOfManifacture)
            .Must(ValidateMaxYearOfManifacture)
            .WithMessage($"O ano deve ser menor ou igual a {_maxYearOfManifacture}");

        RuleFor(ctcv => ctcv.YearOfManifacture)
            .Must(ValiteMinYearOfManifacture)
            .WithMessage($"O ano deve ser maior ou igual a {_minYearOfManifacture}");
    }

    private bool ValiteMinYearOfManifacture(int year)
        => year >= _minYearOfManifacture;

    private bool ValidateMaxYearOfManifacture(int year)
        => year <= _maxYearOfManifacture;
}