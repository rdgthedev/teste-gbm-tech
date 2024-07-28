using GBMProject.Application.Commands.Common.Abstracts;

namespace GBMProject.Application.Commands.Delivery;

public class DeliveryStatusInProgressDeliveryCommand : Command
{
    public DeliveryStatusInProgressDeliveryCommand(Guid deliveryId)
        => DeliveryId = deliveryId;

    public Guid DeliveryId { get; private set; }
}