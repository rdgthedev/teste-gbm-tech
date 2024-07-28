using GBMProject.Application.DTOs.Output;
using GBMProject.Application.Results;
using GBMProject.Core.Interfaces;
using MediatR;

namespace GBMProject.Application.Queries.Driver;

public class GetAllDriversQueryHandler : IRequestHandler<GetAllDriversQuery, Result>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllDriversQueryHandler(
        IUnitOfWork unitOfWork)
        => _unitOfWork = unitOfWork;

    public async Task<Result> Handle(GetAllDriversQuery request, CancellationToken cancellationToken)
    {
        var drivers = await _unitOfWork.Drivers.GetAllAsync(cancellationToken);

        var driversOutput = drivers
            .ToList()
            .Select(d => new DriverDetailsOutputDTO
            {
                Id = d.Id,
                Name = d.Name,
                Cpf = d.Cpf,
                CnhCategory = d.CnhCategory.ToString(),
                BirthDate = d.BirthDate,
                Phone = d.Phone
            });

        return new Result(200, driversOutput);
    }
}