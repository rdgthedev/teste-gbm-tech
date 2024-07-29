using FluentValidation;
using GBMProject.Application.Results;
using GBMProject.Core.Interfaces;
using MediatR;

namespace GBMProject.Application.Commands.Driver;

public class UpdateDriverCommandHandler : IRequestHandler<UpdateDriverCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<UpdateDriverCommand> _validator;

    public UpdateDriverCommandHandler(
        IUnitOfWork unitOfWork,
        IValidator<UpdateDriverCommand> validator)
    {
        _unitOfWork = unitOfWork;
        _validator = validator;
    }

    public async Task<Result> Handle(UpdateDriverCommand request, CancellationToken cancellationToken)
    {
        var result = _validator.Validate(request);

        if (!result.IsValid)
            return new Result(
                400,
                "Não foi possível alterar os dados do motorista",
                result.Errors);

        var phoneExists = await _unitOfWork.Drivers.GetByPhoneAsync(request.Phone, cancellationToken);

        if (phoneExists)
            return new Result(
                409,
                "Não foi possível alterar os dados do motorista",
                "O telefone já está cadastrado");

        var driver = await _unitOfWork.Drivers.GetByIdAsync((Guid)request.DriverId!, cancellationToken);

        if (driver is null)
            return new Result(
                404,
                "Motorista não encontrado",
                "Id inválido");

        driver.ChangeName(request.Name);
        driver.ChangeCnhCategory(request.CnhCategory);
        driver.ChangePhone(request.Phone);

        _unitOfWork.Drivers.Update(driver);
        await _unitOfWork.CommitAsync(cancellationToken);

        return new Result(204);
    }
}