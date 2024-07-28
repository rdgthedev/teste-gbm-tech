using GBMProject.Application.DTOs.Output;
using GBMProject.Application.Results;
using GBMProject.Core.Interfaces;
using MediatR;

namespace GBMProject.Application.Queries.Delivery;

public class GetDeliveryByIdQueryHandler : IRequestHandler<GetDeliveryByIdQuery, Result>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetDeliveryByIdQueryHandler(IUnitOfWork unitOfWork)
        => _unitOfWork = unitOfWork;

    public async Task<Result> Handle(GetDeliveryByIdQuery request, CancellationToken cancellationToken)
    {
        var delivery = await _unitOfWork.Deliveries.GetByIdWithDriversAndTrucksAsync(request.Id, cancellationToken);

        if (delivery is null)
            return new Result(
                404,
                "Não foi possível listar a entrega.",
                "Entrega não encontrada.");

        var deliveryDetailsOutput = new DeliveryDetailsOutputDTO
        {
            Id = delivery.Id,
            DeliveryDate = delivery.DeliveryDate,
            Origin = delivery.Origin,
            Destiny = delivery.Destiny,
            CargoTransported = delivery.CargoTransported,
            DeliveryStatus = delivery.DeliveryStatus.ToString(),
            DriverDetails = new DriverDetailsOutputDTO
            {
                Name = delivery.Driver.Name,
                Cpf = delivery.Driver.Cpf,
                CellPhone = delivery.Driver.CellPhone
            },
            TruckDetails = new TruckDetailsOutputDTO
            {
                Model = delivery.Truck.Model,
                NumberOfAxles = delivery.Truck.NumberOfAxles,
                Color = delivery.Truck.Color,
                Plate = delivery.Truck.Plate
            }
        };
        return new Result(200, deliveryDetailsOutput);
    }
}