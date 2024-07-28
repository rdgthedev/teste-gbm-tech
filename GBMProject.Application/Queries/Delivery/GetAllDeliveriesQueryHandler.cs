using GBMProject.Application.DTOs.Output;
using GBMProject.Application.Results;
using GBMProject.Core.Interfaces;
using MediatR;

namespace GBMProject.Application.Queries.Delivery;

public class GetAllDeliveriesQueryHandler : IRequestHandler<GetAllDeliveriesQuery, Result>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllDeliveriesQueryHandler(IUnitOfWork unitOfWork)
        => _unitOfWork = unitOfWork;

    public async Task<Result> Handle(GetAllDeliveriesQuery request, CancellationToken cancellationToken)
    {
        var deliveries = await _unitOfWork.Deliveries.GetAllWithDriversAndTrucksAsync(cancellationToken);

        var deliveriesDetailsOutputs = deliveries.ToList().Select(d => new DeliveryDetailsOutputDTO
        {
            Id = d.Id,
            DeliveryDate = d.DeliveryDate,
            Origin = d.Origin,
            Destiny = d.Destiny,
            CargoTransported = d.CargoTransported,
            DeliveryStatus = d.DeliveryStatus.ToString(),
            DriverDetails = new DriverDetailsOutputDTO
            {
                Name = d.Driver.Name,
                Cpf = d.Driver.Cpf,
                CellPhone = d.Driver.CellPhone
            },
            TruckDetails = new TruckDetailsOutputDTO
            {
                Model = d.Truck.Model,
                NumberOfAxles = d.Truck.NumberOfAxles,
                Color = d.Truck.Color,
                Plate = d.Truck.Plate
            }
        });

        return new Result(200, deliveriesDetailsOutputs);
    }
}