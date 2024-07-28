using GBMProject.Application.Commands.Common.Abstractions;

namespace GBMProject.Application.Commands.Delivery;

public class DeliveryStatusCanceledCommand : Command
{
    public DeliveryStatusCanceledCommand(Guid deliveryId)
        => DeliveryId = deliveryId;

    public Guid DeliveryId { get; private set; }
}