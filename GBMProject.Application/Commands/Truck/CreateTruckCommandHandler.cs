using FluentValidation;
using GBMProject.Application.Results;
using GBMProject.Core.Interfaces;
using MediatR;

namespace GBMProject.Application.Commands.Truck;

public class CreateTruckCommandHandler : IRequestHandler<CreateTruckCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<CreateTruckCommand> _validator;

    public CreateTruckCommandHandler(
        IUnitOfWork unitOfWork,
        IValidator<CreateTruckCommand> validator)
    {
        _unitOfWork = unitOfWork;
        _validator = validator;
    }

    public async Task<Result> Handle(CreateTruckCommand request, CancellationToken cancellationToken)
    {
        var result = _validator.Validate(request);

        if (!result.IsValid)
            return new Result(
                400,
                "Não foi possível cadastrar o caminhão",
                result.Errors);

        var plateExists = await _unitOfWork.Trucks.PlateExists(request.Plate, cancellationToken);

        if (plateExists)
            return new Result(
                409,
                "Não foi possível cadastrar o caminhão",
                "A placa já existe");

        var truck = new Core.Entities.Truck(request.Plate, request.Model, request.Color, request.NumberOfAxles);

        await _unitOfWork.Trucks.CreateAsync(truck, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);

        return new Result(201, new { Id = truck.Id }, "Caminhão cadastrado com sucesso");
    }
}