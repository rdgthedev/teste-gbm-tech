using GBMProject.Application.DTOs.Output;
using GBMProject.Application.Results;
using GBMProject.Core.Interfaces;
using MediatR;

namespace GBMProject.Application.Queries.Driver;

public class GetDriverByIdQueryHandler : IRequestHandler<GetDriverByIdQuery, Result>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetDriverByIdQueryHandler(IUnitOfWork unitOfWork)
        => _unitOfWork = unitOfWork;

    public async Task<Result> Handle(GetDriverByIdQuery request, CancellationToken cancellationToken)
    {
        var driver = await _unitOfWork.Drivers.GetByIdAsync(request.DriverId, cancellationToken);

        if (driver is null)
            return new Result(
                404,
                "Motorista não encontrado",
                "Id inválido");

        var driverDetailsOutput = new DriverDetailsOutputDTO
        {
            Id = driver.Id,
            Name = driver.Name,
            BirthDate = driver.BirthDate,
            CnhCategory = driver.CnhCategory.ToString(),
            Cpf = driver.Cpf,
            Phone = driver.Phone
        };

        return new Result(200, driverDetailsOutput);
    }
}