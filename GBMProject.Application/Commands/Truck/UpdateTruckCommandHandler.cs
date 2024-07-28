using FluentValidation;
using GBMProject.Application.Results;
using GBMProject.Core.Interfaces;
using MediatR;

namespace GBMProject.Application.Commands.Truck;

public class UpdateTruckCommandHandler : IRequestHandler<UpdateTruckCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<UpdateTruckCommand> _validator;

    public UpdateTruckCommandHandler(
        IUnitOfWork unitOfWork,
        IValidator<UpdateTruckCommand> validator)
    {
        _unitOfWork = unitOfWork;
        _validator = validator;
    }

    public async Task<Result> Handle(UpdateTruckCommand request, CancellationToken cancellationToken) {
        var result = _validator.Validate(request);

        if (!result.IsValid)
            return new Result(
                400,
                "Não foi possível alterar os dados do caminhão",
                result.Errors);

        var truck = await _unitOfWork.Trucks.GetByIdAsync(request.Id, cancellationToken);

        if (truck is null)
            return new Result(
                404,
                "Caminhão não encontrado",
                "Id inválido");

        if (!truck.Plate.Equals(request.Plate))
        {
            var plateExists = await _unitOfWork.Trucks.PlateExists(request.Plate, cancellationToken);

            if (plateExists)
                return new Result(
                    409,
                    "Não foi possível alterar o caminhão",
                    "A placa já cadastrada");
        }

        truck.ChangePlate(request.Plate);
        truck.ChangeColor(request.Color);

        _unitOfWork.Trucks.Update(truck);
        await _unitOfWork.CommitAsync(cancellationToken);

        return new Result(204);
    }
}