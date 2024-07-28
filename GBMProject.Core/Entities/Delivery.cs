using GBMProject.Core.Common;
using GBMProject.Core.Enums;

namespace GBMProject.Core.Entities;

public class Delivery : Entity
{
    public Delivery(
        DateTime deliveryDate,
        string origin,
        string destiny,
        string cargoTransported,
        Guid truckId,
        Guid driverId)
    {
        DeliveryDate = deliveryDate;
        Origin = origin;
        Destiny = destiny;
        CargoTransported = cargoTransported;
        TruckId = truckId;
        DriverId = driverId;
        DeliveryStatus = EDeliveryStatus.Created;
    }

    public DateTime DeliveryDate { get; private set; }
    public string Origin { get; private set; }
    public string Destiny { get; private set; }
    public string CargoTransported { get; private set; }
    public EDeliveryStatus DeliveryStatus { get; private set; }
    public Guid TruckId { get; private set; }
    public Truck Truck { get; private set; }
    public Guid DriverId { get; private set; }
    public Driver Driver { get; private set; }

    public bool ChangeStatusToInProgress()
    {
        if (!DeliveryStatus.Equals(EDeliveryStatus.Created))
            return false;

        DeliveryStatus = EDeliveryStatus.InProgress;
        return true;
    }

    public bool ChangeStatusToFinished()
    {
        if (!DeliveryStatus.Equals(EDeliveryStatus.InProgress))
            return false;

        DeliveryStatus = EDeliveryStatus.Finalized;
        return true;
    }

    public bool ChangeStatusToCanceled()
    {
        if (!DeliveryStatus.Equals(EDeliveryStatus.Created))
            return false;

        DeliveryStatus = EDeliveryStatus.Canceled;
        return true;
    }
}