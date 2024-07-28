using FluentValidation;
using GBMProject.Application.Commands.Driver;
using GBMProject.Application.Commands.Validations.Common;

namespace GBMProject.Application.Commands.Validations;

public class CreateDriverCommandValidator : AbstractValidator<CreateDriverCommand>
{
    public CreateDriverCommandValidator()
    {
        RuleFor(cdcv => cdcv.Name)
            .NotEmpty().WithMessage("O nome é obrigatório")
            .MinimumLength(3).WithMessage("O nome deve possuir no mínimo 3 caracteres");

        RuleFor(cdcv => cdcv.BirthDate)
            .Must(ValidateMinimumValueDate.IsValid).WithMessage("A data de nascimento é obrigatória");
        
        RuleFor(cdcv => cdcv.BirthDate)
            .Must(udc => ValidateMinimumAge.IsValid(udc!.Value, -21))
            .WithMessage("O motorista deve ter pelo menos 21 anos");

        RuleFor(cdcv => cdcv.CnhCategory)
            .NotEmpty().WithMessage("A categoria da CNH é obrigatória")
            .Must(ValidateCategoryType.IsValid).WithMessage("A categoria da CNH é inválida");
        
        RuleFor(cdcv => cdcv.Cpf)
            .NotEmpty().WithMessage("O cpf é obrigatório")
            .Length(11).WithMessage("Cpf deve conter 11 digitos");

        RuleFor(cdcv => cdcv.Phone)
            .NotEmpty().WithMessage("O telefone é obrigatório")
            .Length(11).WithMessage("O telefone deve conter 11 digitos");
    }
    

}