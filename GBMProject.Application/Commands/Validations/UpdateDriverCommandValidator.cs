using FluentValidation;
using GBMProject.Application.Commands.Driver;
using GBMProject.Application.Commands.Validations.Common;
using GBMProject.Core.Enums;

namespace GBMProject.Application.Commands.Validations;

public class UpdateDriverCommandValidator : AbstractValidator<UpdateDriverCommand>
{
    public UpdateDriverCommandValidator()
    {
        RuleFor(udcv => udcv.DriverId)
            .Must(ValidateId.IsValid).WithMessage("O id do motorista é obrigatório.");
        
        RuleFor(udcv => udcv.Name)
            .MinimumLength(3).WithMessage("O nome deve possuir no mínimo 3 caracteres")
            .When(udc => !string.IsNullOrEmpty(udc.Name));
        
        RuleFor(udcv => udcv.CnhCategory)
            .Must(CnhCategoryIsValid)
            .WithMessage("O motorista precisa ter as categorias C, D ou E")
            .When(udc => !string.IsNullOrEmpty(udc.CnhCategory));
            
        RuleFor(udcv => udcv.Phone)
            .Length(11).WithMessage("O telefone deve conter 11 digitos")
            .When(udcv => !string.IsNullOrEmpty(udcv.Phone));
    }
    
    private static bool CnhCategoryIsValid(string category)
        => category switch
        {
            _ when string.Equals(category, nameof(ECnhCategory.C), StringComparison.OrdinalIgnoreCase) => true,
            _ when string.Equals(category, nameof(ECnhCategory.D), StringComparison.OrdinalIgnoreCase) => true,
            _ when string.Equals(category, nameof(ECnhCategory.E), StringComparison.OrdinalIgnoreCase) => true,
            _ => false
        };
}