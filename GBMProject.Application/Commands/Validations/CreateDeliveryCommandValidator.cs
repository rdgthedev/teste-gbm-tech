using FluentValidation;
using GBMProject.Application.Commands.Delivery;
using GBMProject.Application.Commands.Validations.Common;

namespace GBMProject.Application.Commands.Validations;

public class CreateDeliveryCommandValidator : AbstractValidator<CreateDeliveryCommand>
{
    public CreateDeliveryCommandValidator()
    {
        RuleFor(cdcv => cdcv.DeliveryDate)
            .Must(ValidateMinimumValueDate.IsValid).WithMessage("A data de entrega é obrigatória");

        RuleFor(cdcv => cdcv.DeliveryDate)
            .Must(DateSmallerThanCurrent).WithMessage("A data não pode estar no passado")
            .When(cdc => cdc!.DeliveryDate!.Value != default);

        RuleFor(cdcv => cdcv.Origin)
            .NotEmpty().WithMessage("A origem é obrigatória");

        RuleFor(cdcv => cdcv.Destiny)
            .NotEmpty().WithMessage("O destino é obrigatório");

        RuleFor(cdcv => cdcv.CargoTransported)
            .NotEmpty().WithMessage("A carga transportada é obrigatória");

        RuleFor(cdcv => cdcv.TruckId)
            .Must(ValidateId.IsValid).WithMessage("O id do caminhão é obrigatório.");

        RuleFor(cdcv => cdcv.DriverId)
            .Must(ValidateId.IsValid).WithMessage("O id do motorista é obrigatório.");
    }

    private bool DateSmallerThanCurrent(DateTime? deliveryDate)
        => deliveryDate!.Value.Date >= DateTime.Now.Date;
}