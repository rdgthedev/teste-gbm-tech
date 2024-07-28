using FluentValidation;
using GBMProject.Application.Results;
using GBMProject.Core.Enums;
using GBMProject.Core.Interfaces;
using MediatR;

namespace GBMProject.Application.Commands.Driver;

public class CreateDriverCommandHandler : IRequestHandler<CreateDriverCommand, Result>
{
    private readonly IValidator<CreateDriverCommand> _validator;
    private readonly IUnitOfWork _unitOfWork;

    public CreateDriverCommandHandler(
        IUnitOfWork unitOfWork,
        IValidator<CreateDriverCommand> validator)
    {
        _unitOfWork = unitOfWork;
        _validator = validator;
    }

    public async Task<Result> Handle(CreateDriverCommand request, CancellationToken cancellationToken)
    {
        var result =  _validator.Validate(request);

        if (!result.IsValid)
            return new Result(
                400,
                "Não foi possível cadastrar o motorista",
                result.Errors);

        var cpfExists = await _unitOfWork.Drivers.GetByCpfAsync(request.Cpf, cancellationToken);

        if (cpfExists)
            return new Result(
                409,
                "Não foi possível cadastrar o motorista",
                "Cpf já existe");
        
        var phoneExists = await _unitOfWork.Drivers.GetByPhoneAsync(request.Phone, cancellationToken);
        
        if(phoneExists)
            return new Result(
                400,
                "Não foi possível cadastrar o motorista",
                "O telefone já cadastrado");

        var driver = new Core.Entities.Driver(
            request.Name,
            request.Cpf,
            Enum.Parse<ECnhCategory>(request.CnhCategory),
            (DateTime)request.BirthDate!,
            request.Phone);

        await _unitOfWork.Drivers.CreateAsync(driver, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);

        return new Result(201, driver.Id, "Motorista cadastrado com sucesso");
    }
}