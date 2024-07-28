using FluentValidation;
using GBMProject.Application.Results;
using GBMProject.Core.Interfaces;
using MediatR;

namespace GBMProject.Application.Commands.Delivery;

public class CreateDeliveryCommandHandler : IRequestHandler<CreateDeliveryCommand, Result>
{
    private readonly IValidator<CreateDeliveryCommand> _validator;
    private readonly IUnitOfWork _unitOfWork;

    public CreateDeliveryCommandHandler(
        IUnitOfWork unitOfWork,
        IValidator<CreateDeliveryCommand> validator)
    {
        _unitOfWork = unitOfWork;
        _validator = validator;
    }

    public async Task<Result> Handle(CreateDeliveryCommand request, CancellationToken cancellationToken)
    {
        var result = _validator.Validate(request);

        if (!result.IsValid)
            return new Result(
                400,
                "Não foi possível cadastrar a entrega",
                result.Errors);

        var driver = await _unitOfWork.Drivers.GetByIdAsync((Guid)request.DriverId!, cancellationToken);

        if (driver is null)
            return new Result(
                404,
                "Não foi possível cadastrar a entrega",
                "Motorista não encontrado");

        var truck = await _unitOfWork.Trucks.GetByIdAsync((Guid)request.TruckId!, cancellationToken);

        if (truck is null)
            return new Result(
                404,
                "Não foi possível cadastrar a entrega",
                "Caminhão não encontrado");

        var deliveryExists = await _unitOfWork.Deliveries
            .GetCreatedOrInProgressDeliveriesByTruckIdAndDate((Guid)request.TruckId!, (DateTime)request.DeliveryDate!, cancellationToken);

        if (deliveryExists)
            return new Result(
                409,
                "Não foi possível cadastrar a entrega, tente outra data",
                "Já possuí um motorista vinculado a esse caminhão nesta data");

        var delivery = new Core.Entities.Delivery(
            (DateTime)request.DeliveryDate!,
            request.Origin,
            request.Destiny,
            request.CargoTransported,
            (Guid)request.TruckId!,
            (Guid)request.DriverId!);

        await _unitOfWork.Deliveries.CreateAsync(delivery, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);

        return new Result(201, new { id = delivery.Id }, "Entrega cadastrada com sucesso");
    }
}