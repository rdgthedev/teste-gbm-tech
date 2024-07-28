using GBMProject.Application.Results;
using GBMProject.Core.Interfaces;
using MediatR;

namespace GBMProject.Application.Commands.Delivery;

public class DeliveryStatusFinishedCommandHandler : IRequestHandler<DeliveryStatusFinishedCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeliveryStatusFinishedCommandHandler(IUnitOfWork unitOfWork)
        => _unitOfWork = unitOfWork;

    public async Task<Result> Handle(DeliveryStatusFinishedCommand request, CancellationToken cancellationToken)
    {
        var delivery = await _unitOfWork.Deliveries.GetByIdAsync(request.DeliveryId, cancellationToken);

        if (delivery is null)
            return new Result(
                404,
                "Entrega não encontrada",
                "Id inválido");

        var result = delivery.ChangeStatusToFinished();

        if (!result)
            return new Result(
                409,
                "Não foi possível alterar o status da entrega",
                "O status atual não permite ser redefinido para finalizada");

        _unitOfWork.Deliveries.Update(delivery);
        await _unitOfWork.CommitAsync(cancellationToken);

        return new Result(204);
    }
}