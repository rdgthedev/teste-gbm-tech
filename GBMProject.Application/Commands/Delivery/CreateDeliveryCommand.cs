using GBMProject.Application.Commands.Common;
using GBMProject.Application.Commands.Common.Abstractions;

namespace GBMProject.Application.Commands.Delivery;

public class CreateDeliveryCommand : Command
{
    public CreateDeliveryCommand(
        DateTime? deliveryDate,
        string origin,
        string destiny,
        string cargoTransported,
        Guid? truckId,
        Guid? driverId)
    {
        DeliveryDate = deliveryDate ??= DateTime.MinValue;
        Origin = origin;
        Destiny = destiny;
        CargoTransported = cargoTransported;
        TruckId = truckId ??= Guid.Empty;
        DriverId = driverId ??= Guid.Empty;
    }

    public DateTime? DeliveryDate { get; private set; }
    public string Origin { get; private set; }
    public string Destiny { get; private set; }
    public string CargoTransported { get; private set; }
    public Guid? TruckId { get; private set; }
    public Guid? DriverId { get; private set; }
}