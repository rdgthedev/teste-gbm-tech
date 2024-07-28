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
        var delivery = await _unitOfWork.Deliveries.GetByIdAsync(request.Id, cancellationToken);

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
            DeliveryStatus = delivery.DeliveryStatus.ToString()
        };

        return new Result(200, deliveryDetailsOutput);
    }
}