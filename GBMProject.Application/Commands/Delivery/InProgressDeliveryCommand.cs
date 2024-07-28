using GBMProject.Application.Commands.Common;
using GBMProject.Application.Commands.Common.Abstracts;

namespace GBMProject.Application.Commands.Delivery;

public class InProgressDeliveryCommand : Command
{
    public Guid DeliveryId { get; set; }
}