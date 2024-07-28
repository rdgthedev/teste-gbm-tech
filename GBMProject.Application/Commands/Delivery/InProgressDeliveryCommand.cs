using GBMProject.Application.Commands.Common.Abstracts;

namespace GBMProject.Application.Commands.Delivery;

public class InProgressDeliveryCommand : Command
{
    public InProgressDeliveryCommand(Guid deliveryId)
        => DeliveryId = deliveryId;

    public Guid DeliveryId { get; private set; }
}