using GBMProject.Application.Results;
using GBMProject.Core.Interfaces;
using MediatR;

namespace GBMProject.Application.Commands.Delivery;

public class InProgressCommandHandler : IRequestHandler<InProgressDeliveryCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork;

    public InProgressCommandHandler(IUnitOfWork unitOfWork)
        => _unitOfWork = unitOfWork;

    public async Task<Result> Handle(InProgressDeliveryCommand request, CancellationToken cancellationToken)
    {
        var delivery = await _unitOfWork.Deliveries.GetByIdAsync(request.DeliveryId, cancellationToken);

        if (delivery is null)
            return new Result(
                404,
                "Entrega não encontrada",
                "Id inválido");
        
        var result = delivery.ChangeStatusToInProgress();
        
        if(!result)
            return new Result(
                409,
                "Não foi possível alterar o status da entrega",
                "O status atual não pode ser definido como em execução");
        
        _unitOfWork.Deliveries.Update(delivery);
        await _unitOfWork.CommitAsync(cancellationToken);
        
        return new Result(204);
    }
}