using GBMProject.Application.DTOs.Output;
using GBMProject.Application.Results;
using GBMProject.Core.Interfaces;
using MediatR;

namespace GBMProject.Application.Queries.Truck;

public class GetTruckByIdQueryHandler : IRequestHandler<GetTruckByIdQuery, Result>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetTruckByIdQueryHandler(IUnitOfWork unitOfWork)
        => _unitOfWork = unitOfWork;

    public async Task<Result> Handle(GetTruckByIdQuery request, CancellationToken cancellationToken)
    {
        var truck = await _unitOfWork.Trucks.GetByIdAsync(request.TruckId, cancellationToken);

        if (truck is null)
            return new Result(
                404,
                "Caminhão não encontrado",
                "Id inválido");

        var truckDetailsOutput = new TruckDetailsOutputDTO
        {
            Id = truck.Id,
            YearOfManifacture = truck.YearOfManifacture,
            Plate = truck.Plate,
            Color = truck.Color,
            Model = truck.Model,
            NumberOfAxles = truck.NumberOfAxles
        };

        return new Result(200, truckDetailsOutput);
    }
}