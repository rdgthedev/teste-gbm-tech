using GBMProject.Application.Commands.Common.Abstracts;

namespace GBMProject.Application.Commands.Delivery;

public class DeliveryStatusFinishedCommand : Command
{
    public DeliveryStatusFinishedCommand(Guid id)
        => DeliveryId = id;

    public Guid DeliveryId { get; private set; }
}