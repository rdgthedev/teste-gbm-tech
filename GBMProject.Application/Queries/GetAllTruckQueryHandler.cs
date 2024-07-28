using GBMProject.Application.DTOs.Output;
using GBMProject.Application.Results;
using GBMProject.Core.Interfaces;
using MediatR;

namespace GBMProject.Application.Queries.Truck;

public class GetAllTruckQueryHandler : IRequestHandler<GetAllTrucksQuery, Result>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllTruckQueryHandler(IUnitOfWork unitOfWork)
        => _unitOfWork = unitOfWork;

    public async Task<Result> Handle(GetAllTrucksQuery request, CancellationToken cancellationToken)
    {
        var trucks = await _unitOfWork.Trucks.GetAllAsync(cancellationToken);

        var trucksDetailsOutput = trucks
            .ToList()
            .Select(t => new TruckDetailsOutputDTO
            {
                Id = t.Id,
                Plate = t.Plate,
                Model = t.Model,
                YearOfManifacture = t.YearOfManifacture,
                Color = t.Color,
                NumberOfAxles = t.NumberOfAxles
            });

        return new Result(200, trucksDetailsOutput);
    }
}